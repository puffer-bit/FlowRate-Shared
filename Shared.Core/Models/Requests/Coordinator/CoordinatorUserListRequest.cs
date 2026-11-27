using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Every user of the coordinator, banned ones too, without the system user.
/// </summary>
public record CoordinatorUserListRequest() : CoordinatorRequest
{
    public CoordinatorUserListResponse ToResponse(List<UserDTO> users)
        => new(RequestId, users);
}
