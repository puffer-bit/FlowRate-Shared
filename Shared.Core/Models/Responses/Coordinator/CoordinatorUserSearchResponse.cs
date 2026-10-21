using Shared.Enums;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorUserSearchResponse(
    Guid RequestId,
    List<UserDTO> Users,
    UsersRequestResult Result) : CoordinatorResponse(RequestId);
