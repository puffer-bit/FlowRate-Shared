using Shared.Enums.User;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

/// <param name="User">The user as they are now, when the state was set.</param>
public record CoordinatorSetUserStateResponse(
    Guid RequestId,
    ModerateUserResult Result,
    int UserTargetId,
    UserDTO? User) : CoordinatorResponse(RequestId);
