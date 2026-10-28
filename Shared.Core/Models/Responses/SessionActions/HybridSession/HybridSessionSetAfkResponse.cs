namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionSetAfkResponse(
    Guid RequestId,
    bool IsAfk) : HybridSessionResponse(RequestId);
