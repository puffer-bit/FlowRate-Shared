using Shared.Enums.User;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

/// <param name="User">The sender as they are now, when the status was set.</param>
public record CoordinatorSetUserStatusResponse(
    Guid RequestId,
    SetUserStatusResult Result,
    UserDTO? User) : CoordinatorResponse(RequestId);
