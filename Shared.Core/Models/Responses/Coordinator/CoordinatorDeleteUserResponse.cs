namespace Shared.Models.Responses.Coordinator;

public record CoordinatorDeleteUserResponse(
    Guid RequestId,
    int UserTargetId) : CoordinatorResponse(RequestId);
