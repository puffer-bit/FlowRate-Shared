using System;
using System.Text.Json;
using Shared.Models;
using Shared.Models.DataTransferObjects.ChatSession.Messages;
using Shared.Models.DataTransferObjects.ChatSession.Messages.Content;
using Shared.Models.Notifications.SessionInfo;
using Shared.Serialization;
using Xunit;

namespace Shared.Tests;

public sealed class ChatSystemMessageTests
{
    [Theory]
    [InlineData(ChatSystemEventKind.MemberAdded)]
    [InlineData(ChatSystemEventKind.MemberRemoved)]
    [InlineData(ChatSystemEventKind.MemberLeft)]
    [InlineData(ChatSystemEventKind.CallStarted)]
    [InlineData(ChatSystemEventKind.CallEnded)]
    public void SystemEvent_RoundTripsThroughAotProtocolContext(ChatSystemEventKind kind)
    {
        var call = kind is ChatSystemEventKind.CallStarted or ChatSystemEventKind.CallEnded;
        var content = new ChatSystemContent
        {
            Kind = kind, ActorUserId = 1,
            SubjectUserId = call ? null : kind == ChatSystemEventKind.MemberLeft ? 1 : 2,
            CallId = call ? Guid.NewGuid() : null
        };
        BaseMessage notification = new ChatSessionMessageAddedNotification(10, new ChatMessageDTO
        {
            Id = 20, ChatId = 10, SenderUserId = -1, SentTime = DateTime.UtcNow,
            IsSystem = true, IsReadOnly = true, SystemContent = content,
            TextContent = new ChatTextContentDTO { Text = content.ToFallbackText() },
            ReplyContent = null!, Images = [], Video = [], Files = [], Reactions = [], UsersIds = []
        });
        var json = JsonSerializer.Serialize(notification, ProtocolJsonContext.Default.BaseMessage);
        var loaded = Assert.IsType<ChatSessionMessageAddedNotification>(
            JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage));
        Assert.Equal(content, loaded.Message.SystemContent);
        Assert.True(loaded.Message.IsSystem);
        Assert.Equal(content.ToFallbackText(), loaded.Message.TextContent.Text);
        // An older DTO without event metadata still reads as a system message.
        var legacy = json.Replace(JsonSerializer.Serialize(content, ProtocolJsonContext.Default.ChatSystemContent), "null");
        var oldMessage = Assert.IsType<ChatSessionMessageAddedNotification>(
            JsonSerializer.Deserialize(legacy, ProtocolJsonContext.Default.BaseMessage));
        Assert.Null(oldMessage.Message.SystemContent);
        Assert.True(oldMessage.Message.IsSystem);
    }

    [Fact]
    public void EventData_RejectsInvalidCombinations()
    {
        var member = new ChatSystemContent { Kind = ChatSystemEventKind.MemberAdded, ActorUserId = 1, SubjectUserId = 2 };
        Assert.True(member.IsValid());
        Assert.False((member with { Kind = (ChatSystemEventKind)999 }).IsValid());
        Assert.False((member with { ActorUserId = null }).IsValid());
        Assert.False((member with { SubjectUserId = -1 }).IsValid());
        Assert.False((member with { CallId = Guid.NewGuid() }).IsValid());
        Assert.False((member with { Kind = ChatSystemEventKind.MemberLeft }).IsValid());
        var ended = new ChatSystemContent { Kind = ChatSystemEventKind.CallEnded, CallId = Guid.NewGuid() };
        Assert.True(ended.IsValid());
        Assert.False((ended with { Kind = ChatSystemEventKind.CallStarted }).IsValid());
        Assert.False((ended with { CallId = Guid.Empty }).IsValid());
        Assert.False((ended with { SubjectUserId = 2 }).IsValid());
    }
}
