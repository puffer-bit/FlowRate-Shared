using System.ComponentModel.DataAnnotations;
using Shared.Enums.Coordinator;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Changes a role. Accepted only while <see cref="ExpectedRevision"/> is the current revision of the role,
/// so a change made meanwhile elsewhere is never overwritten unseen.
/// </summary>
public record CoordinatorUpdateRoleRequest(
    [property: EntityId] int RoleId,
    [property: Required] RoleUpdateDTO Role,
    long ExpectedRevision) : CoordinatorRequest
{
    public CoordinatorRoleResponse ToResponse(CoordinatorRoleResult result, RoleDTO? role = null)
        => new(RequestId, result, role);
}
