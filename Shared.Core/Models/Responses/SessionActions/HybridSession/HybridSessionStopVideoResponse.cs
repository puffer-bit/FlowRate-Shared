using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStopVideoResponse(
    Guid RequestId,
    StopVideoShareResult Result) : HybridSessionResponse(RequestId);
