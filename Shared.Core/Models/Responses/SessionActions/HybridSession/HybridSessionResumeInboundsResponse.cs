namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionResumeInboundsResponse(Guid RequestId) : HybridSessionResponse(RequestId);
