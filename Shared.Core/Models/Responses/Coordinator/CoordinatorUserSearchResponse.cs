using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorUserSearchResponse(
    Guid RequestId,
    List<UserDTO> Users) : CoordinatorResponse(RequestId);
