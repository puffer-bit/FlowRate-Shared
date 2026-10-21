using System.ComponentModel.DataAnnotations;
using Shared.Enums.Coordinator;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;

namespace Shared.Models.Requests.Coordinator;

/// <summary>Creates a role right above the everyone role.</summary>
public record CoordinatorCreateRoleRequest([property: Required] RoleUpdateDTO Role) : CoordinatorRequest
{
    public CoordinatorRoleResponse ToResponse(CoordinatorRoleResult result, RoleDTO? role = null)
        => new(RequestId, result, role);
}
