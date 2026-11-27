using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorUserListResponse(
    Guid RequestId,
    List<UserDTO> Users) : CoordinatorResponse(RequestId);
