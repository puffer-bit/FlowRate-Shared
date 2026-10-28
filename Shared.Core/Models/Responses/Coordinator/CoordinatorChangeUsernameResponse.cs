using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorChangeUsernameResponse(
    Guid RequestId,
    UserDTO User) : CoordinatorResponse(RequestId);
