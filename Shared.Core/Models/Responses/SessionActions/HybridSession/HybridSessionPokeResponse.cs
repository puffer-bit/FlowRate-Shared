namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionPokeResponse(
    Guid RequestId,
    int UserTargetId) : HybridSessionResponse(RequestId);
