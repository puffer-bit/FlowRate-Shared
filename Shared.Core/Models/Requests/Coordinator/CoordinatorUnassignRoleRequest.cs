using Shared.Enums.Coordinator;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>Takes a role from a user. Taking a role the user does not have changes nothing.</summary>
public record CoordinatorUnassignRoleRequest(
    [property: EntityId] int UserTargetId,
    [property: EntityId] int RoleId) : CoordinatorRequest
{
    public CoordinatorUserRolesResponse ToResponse(CoordinatorRoleResult result, IList<int>? roleIds = null)
        => new(RequestId, result, UserTargetId, roleIds);
}
