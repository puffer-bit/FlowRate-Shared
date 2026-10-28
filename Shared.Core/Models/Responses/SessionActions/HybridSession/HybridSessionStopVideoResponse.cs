namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStopVideoResponse(Guid RequestId) : HybridSessionResponse(RequestId);
