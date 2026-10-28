namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStopVoiceResponse(Guid RequestId) : HybridSessionResponse(RequestId);
