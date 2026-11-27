using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

/// <param name="User">The sender as they are now, when the status was set.</param>
public record CoordinatorSetUserStatusResponse(
    Guid RequestId,
    UserDTO User) : CoordinatorResponse(RequestId);
