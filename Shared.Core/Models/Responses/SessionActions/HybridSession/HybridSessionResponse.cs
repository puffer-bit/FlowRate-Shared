namespace Shared.Models.Responses.SessionActions.HybridSession;

public abstract record HybridSessionResponse(Guid RequestId) : Response(RequestId);
