using Shared.Enums.User;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorChangeUsernameResponse(
    Guid RequestId,
    ChangeUsernameResult Result,
    UserDTO? User) : CoordinatorResponse(RequestId);
