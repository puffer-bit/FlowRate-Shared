using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;
using Shared.Models;
using Shared.Models.Push;
using Shared.Protocol;
using Shared.Serialization;
using Xunit;

namespace Shared.Tests;

/// <summary>
/// Keeps <see cref="ProtocolVersion.Current"/> honest: the message schema is compared with the snapshot in
/// protocol-schema.txt, so no change of it reaches the wire without someone deciding about the version.
/// </summary>
public class ProtocolSchemaTests
{
    private const string SnapshotName = "protocol-schema.txt";
    private const string ReceivedName = "protocol-schema.received.txt";

    [Fact]
    public void Schema_MatchesTheSnapshotOfItsVersion()
    {
        var directory = SourceDirectory();
        var snapshot = Path.Combine(directory, SnapshotName);
        var received = Path.Combine(directory, ReceivedName);
        var actual = Describe();

        if (File.Exists(snapshot) && File.ReadAllText(snapshot).ReplaceLineEndings("\n") == actual)
        {
            File.Delete(received);
            return;
        }

        File.WriteAllText(received, actual);
        Assert.Fail($"""
            The message schema differs from {SnapshotName}; the new one is in {ReceivedName} next to it.
            If old clients or servers cannot read the new messages, raise ProtocolVersion.Current
            (and MinSupported once the server stops serving the old version). Then replace {SnapshotName}
            with {ReceivedName}.
            """);
    }

    [Fact]
    public void MinSupported_IsAPositiveVersionNotAboveCurrent()
    {
        Assert.InRange(ProtocolVersion.MinSupported, 1, ProtocolVersion.Current);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("0", false)]
    [InlineData("-1", false)]
    [InlineData(" 1", false)]
    [InlineData("1.0", false)]
    [InlineData("abc", false)]
    public void IsSupported_RefusesWhatIsNotAVersion(string? header, bool expected)
    {
        Assert.Equal(expected, ProtocolVersion.IsSupported(header));
    }

    [Fact]
    public void IsSupported_ServesExactlyTheRangeOfTheServer()
    {
        Assert.True(ProtocolVersion.IsSupported(ProtocolVersion.Format(ProtocolVersion.Current)));
        Assert.True(ProtocolVersion.IsSupported(ProtocolVersion.Format(ProtocolVersion.MinSupported)));
        Assert.False(ProtocolVersion.IsSupported(ProtocolVersion.Format(ProtocolVersion.Current + 1)));
        Assert.False(ProtocolVersion.IsSupported(ProtocolVersion.Format(ProtocolVersion.MinSupported - 1)));
    }

    /// <summary>
    /// Every type reachable from the protocol roots: object properties with their types, derived types
    /// with their discriminators, collection items and enum numbers - what decides how JSON is read.
    /// </summary>
    private static string Describe()
    {
        var lines = new SortedSet<string>(StringComparer.Ordinal);
        var seen = new HashSet<Type>();
        var pending = new Queue<Type>([typeof(BaseMessage), typeof(PushReplyRequest), typeof(PushMarkReadRequest)]);

        while (pending.TryDequeue(out var type))
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (!seen.Add(type))
                continue;

            var name = NameOf(type);
            if (type.IsEnum)
            {
                var members = Enum.GetValues(type).Cast<object>()
                    .Select(value => $"{Enum.GetName(type, value)}={Convert.ToInt64(value)}");
                lines.Add($"{name} = enum {string.Join(", ", members)}");
                continue;
            }

            var info = ProtocolJsonContext.Default.GetTypeInfo(type)
                       ?? throw new InvalidOperationException($"{name} is not in ProtocolJsonContext");

            switch (info.Kind)
            {
                case JsonTypeInfoKind.Object:
                    foreach (var property in info.Properties)
                    {
                        lines.Add($"{name}.{property.Name}: {NameOf(property.PropertyType)}");
                        pending.Enqueue(property.PropertyType);
                    }

                    foreach (var derived in info.PolymorphismOptions?.DerivedTypes ?? [])
                    {
                        lines.Add($"{name} <- \"{derived.TypeDiscriminator}\": {NameOf(derived.DerivedType)}");
                        pending.Enqueue(derived.DerivedType);
                    }
                    break;
                case JsonTypeInfoKind.Enumerable:
                    pending.Enqueue(info.ElementType!);
                    break;
                case JsonTypeInfoKind.Dictionary:
                    pending.Enqueue(info.KeyType!);
                    pending.Enqueue(info.ElementType!);
                    break;
            }
        }

        return $"version {ProtocolVersion.Current}\n" + string.Join("\n", lines) + "\n";
    }

    private static string NameOf(Type type)
    {
        if (Nullable.GetUnderlyingType(type) is { } underlying)
            return NameOf(underlying) + "?";
        if (type.IsArray)
            return NameOf(type.GetElementType()!) + "[]";
        if (!type.IsGenericType)
            return type.Name;

        var name = type.Name[..type.Name.IndexOf('`')];
        return $"{name}<{string.Join(", ", type.GetGenericArguments().Select(NameOf))}>";
    }

    private static string SourceDirectory([CallerFilePath] string path = "") => Path.GetDirectoryName(path)!;
}
