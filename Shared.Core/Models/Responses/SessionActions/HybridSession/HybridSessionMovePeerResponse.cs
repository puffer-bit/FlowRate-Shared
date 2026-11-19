namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionMovePeerResponse(
    Guid RequestId,
    int SessionId,
    int TargetUserId,
    int TargetSessionId) : HybridSessionResponse(RequestId);
