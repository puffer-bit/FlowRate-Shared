using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Enums.Coordinator;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Puts the roles in a new order, lowest first. Lists every role except the everyone role, so a role added
/// or deleted meanwhile makes it a conflict.
/// </summary>
public record CoordinatorReorderRolesRequest(
    [property: Required, ItemCount(Limits.RolesMaxCount)] IList<int> RoleIds) : CoordinatorRequest
{
    public CoordinatorRolesResponse ToResponse(CoordinatorRoleResult result, IList<RoleDTO>? roles = null)
        => new(RequestId, result, roles, Permission.None);
}
