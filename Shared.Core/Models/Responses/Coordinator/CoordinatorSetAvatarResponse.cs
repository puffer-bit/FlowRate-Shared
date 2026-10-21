using Shared.Enums.User;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorSetAvatarResponse(
    Guid RequestId,
    SetAvatarResult Result,
    UserDTO? User) : CoordinatorResponse(RequestId);
