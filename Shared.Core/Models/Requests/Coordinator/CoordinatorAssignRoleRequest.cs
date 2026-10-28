using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>Gives a role to a user. Giving a role the user already has changes nothing.</summary>
public record CoordinatorAssignRoleRequest(
    [property: EntityId] int UserTargetId,
    [property: EntityId] int RoleId) : CoordinatorRequest
{
    public CoordinatorUserRolesResponse ToResponse(IList<int> roleIds)
        => new(RequestId, UserTargetId, roleIds);
}
