using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStopVoiceResponse(
    Guid RequestId,
    StopVoiceShareResult Result) : HybridSessionResponse(RequestId);
