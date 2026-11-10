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
public sealed class DirectCallProtocolTests
{
    [Fact]
    public void InvitationRoundTripsWithoutExposingDeviceIdentifiers()
    {
        var call = new DirectCallDTO(Guid.NewGuid(), 7, 1, 2, DirectCallState.Ringing, null,
            DateTimeOffset.UtcNow, null, false, null);
        BaseMessage notification = new DirectCallChangedNotification(call);
        var json = JsonSerializer.Serialize(notification, ProtocolJsonContext.Default.BaseMessage);
        var decoded = Assert.IsType<DirectCallChangedNotification>(JsonSerializer.Deserialize(json,
            ProtocolJsonContext.Default.BaseMessage));
        Assert.Equal(call, decoded.Call);
        Assert.DoesNotContain("ConnectionId", json);
    }

    [Fact]
    public void CallRequestRejectsEmptyIdsAndUnknownActions()
    {
        Assert.False(MessageValidator.TryValidate(new DirectCallRequest(7, Guid.Empty, DirectCallAction.Start), out _));
        Assert.False(MessageValidator.TryValidate(new DirectCallRequest(7, Guid.NewGuid(), (DirectCallAction)99), out _));
        Assert.False(MessageValidator.TryValidate(new DirectCallRequest(-1, Guid.NewGuid(), DirectCallAction.Start), out _));
        Assert.True(MessageValidator.TryValidate(new DirectCallRequest(7, Guid.NewGuid(), DirectCallAction.Start), out _));
    }

    [Theory]
    [InlineData(DirectCallEndReason.Completed, 65)]
    [InlineData(DirectCallEndReason.Missed, null)]
    [InlineData(DirectCallEndReason.Declined, null)]
    public void OutcomesAreValidSystemEvents(DirectCallEndReason reason, int? seconds)
    {
        var content = new ChatSystemContent { Kind = ChatSystemEventKind.CallEnded, CallId = Guid.NewGuid(),
            CallEndReason = reason, CallDurationSeconds = seconds };
        Assert.True(content.IsValid());
        Assert.False((content with { CallDurationSeconds = -1 }).IsValid());
        Assert.False((content with { Kind = ChatSystemEventKind.CallStarted, ActorUserId = 1 }).IsValid());
    }
}
