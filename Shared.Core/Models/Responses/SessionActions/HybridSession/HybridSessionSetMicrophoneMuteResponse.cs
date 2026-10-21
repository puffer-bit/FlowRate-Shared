using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionSetMicrophoneMuteResponse(
    Guid RequestId,
    SetMicrophoneMuteResult Result,
    bool IsMuted) : HybridSessionResponse(RequestId);
