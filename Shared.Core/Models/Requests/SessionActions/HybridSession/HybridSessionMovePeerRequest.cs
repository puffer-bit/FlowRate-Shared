using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

/// <summary>
/// Moves a peer of the session <paramref name="SessionId"/> to the hybrid session <paramref name="TargetSessionId"/>,
/// past its capacity and approval; a peer still waiting for approval can be moved too. The peer leaves
/// <paramref name="SessionId"/> at once and has a place kept for it in the target, where its client joins as usual
/// (<see cref="HybridSessionJoinRequest"/>) after <see cref="Notifications.SessionInfo.HybridSessionPeerMovedOutNotification"/>.
/// A place the client does not take in time is given up.
/// </summary>
public record HybridSessionMovePeerRequest(
    [property: EntityId] int SessionId,
    [property: EntityId] int TargetUserId,
    [property: EntityId] int TargetSessionId)
    : HybridSessionRequest
{
    public HybridSessionMovePeerResponse ToResponse()
        => new(RequestId, SessionId, TargetUserId, TargetSessionId);
}
