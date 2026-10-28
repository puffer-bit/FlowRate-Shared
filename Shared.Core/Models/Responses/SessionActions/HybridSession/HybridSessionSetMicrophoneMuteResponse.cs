namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionSetMicrophoneMuteResponse(
    Guid RequestId,
    bool IsMuted) : HybridSessionResponse(RequestId);
