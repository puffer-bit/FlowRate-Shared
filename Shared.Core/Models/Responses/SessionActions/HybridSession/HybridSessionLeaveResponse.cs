namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionLeaveResponse(Guid RequestId)
    : HybridSessionResponse(RequestId);
