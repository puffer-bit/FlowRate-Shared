using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStopScreenShareResponse(
    Guid RequestId,
    StopScreenShareResult Result) : HybridSessionResponse(RequestId);
