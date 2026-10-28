namespace Shared.Models.Responses.Coordinator;

public record CoordinatorDeleteUserResponse(
    Guid RequestId,
    int TargetUserId) : CoordinatorResponse(RequestId);
