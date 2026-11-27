namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionSetSoundMuteResponse(
    Guid RequestId,
    bool IsMuted) : HybridSessionResponse(RequestId);
