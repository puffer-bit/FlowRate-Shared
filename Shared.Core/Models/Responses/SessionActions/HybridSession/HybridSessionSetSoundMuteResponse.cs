using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionSetSoundMuteResponse(
    Guid RequestId,
    SetSoundMuteResult Result,
    bool IsMuted) : HybridSessionResponse(RequestId);
