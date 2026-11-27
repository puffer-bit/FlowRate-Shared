namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionPokeResponse(
    Guid RequestId,
    int TargetUserId) : HybridSessionResponse(RequestId);
