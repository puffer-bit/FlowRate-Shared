using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Serialization;

/// <summary>
/// A moment on the wire: whole milliseconds since the Unix epoch, always UTC. A number is smaller than an
/// ISO string, has no offset or kind to get wrong, and needs no parsing on either side.
/// </summary>
public sealed class UnixMillisecondsConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Number || !reader.TryGetInt64(out var milliseconds))
            throw new JsonException("A time is a whole number of milliseconds since the Unix epoch.");
        try
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
        }
        catch (Exception e) when (e is ArgumentOutOfRangeException or OverflowException)
        {
            throw new JsonException("The time is out of range.");
        }
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
}

/// <summary>A length of time on the wire: whole milliseconds.</summary>
public sealed class MillisecondsConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Number || !reader.TryGetInt64(out var milliseconds))
            throw new JsonException("A duration is a whole number of milliseconds.");
        try
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }
        catch (Exception e) when (e is ArgumentOutOfRangeException or OverflowException)
        {
            throw new JsonException("The duration is out of range.");
        }
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options) =>
        writer.WriteNumberValue((long)value.TotalMilliseconds);
}
