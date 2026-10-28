using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;

namespace Shared.Models.Requests.Coordinator;

/// <summary>The roles of the coordinator and the permissions the sender has.</summary>
public record CoordinatorRolesRequest() : CoordinatorRequest
{
    public CoordinatorRolesResponse ToResponse(IList<RoleDTO> roles,
        Permission permissions = Permission.None)
        => new(RequestId, roles, permissions);
}
