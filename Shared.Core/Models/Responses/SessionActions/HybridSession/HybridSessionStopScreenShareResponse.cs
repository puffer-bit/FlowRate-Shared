namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStopScreenShareResponse(Guid RequestId) : HybridSessionResponse(RequestId);
