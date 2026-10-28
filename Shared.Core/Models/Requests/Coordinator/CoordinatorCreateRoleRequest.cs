using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;

namespace Shared.Models.Requests.Coordinator;

/// <summary>Creates a role right above the everyone role.</summary>
public record CoordinatorCreateRoleRequest([property: Required] RoleUpdateDTO Role) : CoordinatorRequest
{
    public CoordinatorRoleResponse ToResponse(RoleDTO role)
        => new(RequestId, role);
}
