using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorUserInfoResponse(
    Guid RequestId,
    IDictionary<int, UserDTO> Users) : CoordinatorResponse(RequestId);
