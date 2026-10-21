using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Shared.Enums;
using Shared.Models;
using Shared.Models.DataTransferObjects.ChatSession;
using Shared.Models.DataTransferObjects.ChatSession.User;
using Shared.Models.Notifications.SessionInfo;
using Shared.Serialization;
using Xunit;

namespace Shared.Tests;

/// <summary>
/// Every message travels as <see cref="BaseMessage"/>, so System.Text.Json can only write and read
/// the concrete types listed in its <see cref="JsonDerivedTypeAttribute"/> registrations.
/// </summary>
public class BaseMessageRegistrationTests
{
    // The wire format before the source-generated context; both must produce the same JSON
    private static readonly JsonSerializerOptions ReflectionOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        MaxDepth = 16,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
    };

    private static readonly JsonTypeInfo<BaseMessage> BaseMessageInfo = ProtocolJsonContext.Default.BaseMessage;

    private static readonly JsonDerivedTypeAttribute[] Registrations =
        typeof(BaseMessage).GetCustomAttributes<JsonDerivedTypeAttribute>().ToArray();

    public static TheoryData<Type> ConcreteMessageTypes()
    {
        var data = new TheoryData<Type>();
        foreach (var type in typeof(BaseMessage).Assembly.GetTypes()
                     .Where(t => typeof(BaseMessage).IsAssignableFrom(t) && !t.IsAbstract)
                     .OrderBy(t => t.FullName))
            data.Add(type);
        return data;
    }

    [Theory]
    [MemberData(nameof(ConcreteMessageTypes))]
    public void ConcreteMessage_IsRegistered(Type type)
    {
        Assert.True(Registrations.Any(r => r.DerivedType == type),
            $"{type.Name} is not registered as a JsonDerivedType of BaseMessage");
    }

    [Fact]
    public void Discriminators_MatchTypeNames()
    {
        foreach (var registration in Registrations)
            Assert.Equal(registration.DerivedType.Name, registration.TypeDiscriminator);
    }

    [Fact]
    public void Discriminators_AreUnique()
    {
        var duplicates = Registrations
            .GroupBy(r => r.TypeDiscriminator)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        Assert.Empty(duplicates);
    }

    [Theory]
    [MemberData(nameof(ConcreteMessageTypes))]
    public void ConcreteMessage_RoundTripsThroughBaseMessage(Type type)
    {
        // The constructor is skipped on purpose: the test is about the type map, not about values.
        var message = (BaseMessage)RuntimeHelpers.GetUninitializedObject(type);

        var json = JsonSerializer.Serialize(message, BaseMessageInfo);
        var restored = JsonSerializer.Deserialize(json, BaseMessageInfo);

        Assert.NotNull(restored);
        Assert.Equal(type, restored.GetType());
        Assert.Equal(json, JsonSerializer.Serialize(restored, BaseMessageInfo));
    }

    [Theory]
    [MemberData(nameof(ConcreteMessageTypes))]
    public void SourceGeneratedJson_MatchesReflectionJson(Type type)
    {
        var message = (BaseMessage)RuntimeHelpers.GetUninitializedObject(type);

        Assert.Equal(
            JsonSerializer.Serialize(message, ReflectionOptions),
            JsonSerializer.Serialize(message, BaseMessageInfo));
    }

    [Fact]
    public void ChatUserAdminFlag_SurvivesRoundTrip()
    {
        var session = new ChatSessionDTO
        {
            Id = 1,
            CoordinatorInstanceId = "c",
            Name = "Chat",
            Capacity = 10,
            SessionType = SessionType.Chat,
            CreatorUserId = 7,
            Users = new Dictionary<int, TextChatUserDTO> { [7] = new() { UserId = 7, IsAdmin = true } }
        };

        var json = JsonSerializer.Serialize<BaseMessage>(new ChatSessionCreatedNotification(session), BaseMessageInfo);
        var restored = Assert.IsType<ChatSessionCreatedNotification>(JsonSerializer.Deserialize(json, BaseMessageInfo));

        Assert.True(restored.Session.Users[7].IsAdmin);
    }
}
