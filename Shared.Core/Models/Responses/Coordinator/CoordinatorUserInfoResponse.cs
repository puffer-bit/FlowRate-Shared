using Shared.Enums;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorUserInfoResponse(
    Guid RequestId,
    IDictionary<int, UserDTO> UserList,
    UsersRequestResult Result) : CoordinatorResponse(RequestId);
