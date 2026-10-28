using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorSetAvatarResponse(
    Guid RequestId,
    UserDTO User) : CoordinatorResponse(RequestId);
