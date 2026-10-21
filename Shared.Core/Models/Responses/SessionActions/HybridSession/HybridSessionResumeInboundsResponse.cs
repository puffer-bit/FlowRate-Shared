using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionResumeInboundsResponse(
    Guid RequestId,
    ResumeInboundsResult Result) : HybridSessionResponse(RequestId);
