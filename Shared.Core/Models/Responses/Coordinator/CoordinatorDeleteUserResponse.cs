using Shared.Enums.User;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorDeleteUserResponse(
    Guid RequestId,
    ModerateUserResult Result,
    int UserTargetId) : CoordinatorResponse(RequestId);
