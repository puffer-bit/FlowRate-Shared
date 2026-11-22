using System;
using System.Text.Json;
using Shared.Models;
using Shared.Models.DataTransferObjects;
using Shared.Models.DataTransferObjects.ChatSession.Messages;
using Shared.Models.Notifications.SessionInfo;
using Shared.Models.Requests.SessionActions.ChatSession;
using Shared.Serialization;
using Shared.Validation;
using Xunit;
namespace Shared.Tests;
public sealed class ChatCallProtocolTests
{
    [Fact]
    public void InvitationRoundTripsWithoutExposingDeviceIdentifiers()
    {
        var call = new ChatCallDTO(Guid.NewGuid(), 7, 1, ChatCallState.Active, null,
            DateTimeOffset.FromUnixTimeMilliseconds(1_795_000_000_123), null,
            [new ChatCallParticipantDTO(1, ChatCallParticipantState.Joined),
             new ChatCallParticipantDTO(2, ChatCallParticipantState.Ringing)], false, null);
        BaseMessage notification = new ChatCallChangedNotification(call);
        var json = JsonSerializer.Serialize(notification, ProtocolJsonContext.Default.BaseMessage);
        var decoded = Assert.IsType<ChatCallChangedNotification>(JsonSerializer.Deserialize(json,
            ProtocolJsonContext.Default.BaseMessage));
        Assert.Equal(call with { Participants = [] }, decoded.Call with { Participants = [] });
        Assert.Equal(call.Participants, decoded.Call.Participants);
        Assert.DoesNotContain("ConnectionId", json);
    }

    [Fact]
    public void CallRequestRejectsEmptyIdsAndUnknownActions()
    {
        Assert.False(MessageValidator.TryValidate(new ChatCallRequest(7, Guid.Empty, ChatCallAction.Start), out _));
        Assert.False(MessageValidator.TryValidate(new ChatCallRequest(7, Guid.NewGuid(), (ChatCallAction)99), out _));
        Assert.False(MessageValidator.TryValidate(new ChatCallRequest(-1, Guid.NewGuid(), ChatCallAction.Start), out _));
        Assert.False(MessageValidator.TryValidate(new ChatCallRequest(7, Guid.NewGuid(), ChatCallAction.Ring, 0), out _));
        Assert.True(MessageValidator.TryValidate(new ChatCallRequest(7, Guid.NewGuid(), ChatCallAction.Start), out _));
        Assert.True(MessageValidator.TryValidate(new ChatCallRequest(7, Guid.NewGuid(), ChatCallAction.Ring, 3), out _));
    }

    [Theory]
    [InlineData(ChatCallEndReason.Completed, 65)]
    [InlineData(ChatCallEndReason.Missed, null)]
    [InlineData(ChatCallEndReason.Declined, null)]
    public void OutcomesAreValidSystemEvents(ChatCallEndReason reason, int? seconds)
    {
        var content = new ChatSystemContent { Kind = ChatSystemEventKind.CallEnded, CallId = Guid.NewGuid(),
            CallEndReason = reason, CallDurationSeconds = seconds };
        Assert.True(content.IsValid());
        Assert.False((content with { CallDurationSeconds = -1 }).IsValid());
        Assert.False((content with { Kind = ChatSystemEventKind.CallStarted, ActorUserId = 1 }).IsValid());
    }
}
