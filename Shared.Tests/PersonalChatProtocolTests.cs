using System;
using System.Linq;
using System.Text.Json;
using Shared.Enums;
using Shared.Models;
using Shared.Models.Requests.SessionActions.Generic;
using Shared.Models.Requests.SessionActions.Generic.Models;
using Shared.Models.Requests.SessionActions.Generic.Models.SessionData;
using Shared.Serialization;
using Shared.Validation;
using Xunit;

namespace Shared.Tests;

public sealed class PersonalChatProtocolTests
{
    [Fact]
    public void PrivateCreation_CannotDisguiseAHybridSession()
    {
        var request = new CreateSessionRequest(new CreateSessionModel("Private", -1,
            new ChatSessionCreateData(Kind: ChatKind.Direct, MemberIds: [2]) { SessionType = SessionType.Hybrid }));
        Assert.False(MessageValidator.TryValidate(request, out _));
    }

    [Fact]
    public void DirectCreation_RoundTripsThroughSourceGeneratedProtocol()
    {
        BaseMessage request = new CreateSessionRequest(new CreateSessionModel("Private", -1,
            new ChatSessionCreateData(Kind: ChatKind.Direct, MemberIds: [2])));
        var json = JsonSerializer.Serialize(request, ProtocolJsonContext.Default.BaseMessage);
        var decoded = Assert.IsType<CreateSessionRequest>(JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage));
        var data = Assert.IsType<ChatSessionCreateData>(decoded.CreateSessionModel.SessionData);
        Assert.Equal(ChatKind.Direct, data.Kind);
        Assert.Equal(new[] { 2 }, data.MemberIds);
        Assert.True(MessageValidator.TryValidate(decoded, out _));
    }

    [Fact]
    public void GroupChange_BoundsMembershipAndRejectsInvalidIds()
    {
        Assert.False(MessageValidator.TryValidate(new ChangeGroupRequest(1, "Group", new int[101], 1), out _));
        Assert.False(MessageValidator.TryValidate(new ChangeGroupRequest(1, "Group", [1, -2], 1), out _));
        Assert.True(MessageValidator.TryValidate(new ChangeGroupRequest(1, "Group", [1, 2], 1), out _));
    }
}
