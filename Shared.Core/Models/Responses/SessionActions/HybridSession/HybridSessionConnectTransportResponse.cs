namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionConnectTransportResponse(Guid RequestId) : HybridSessionResponse(RequestId);
