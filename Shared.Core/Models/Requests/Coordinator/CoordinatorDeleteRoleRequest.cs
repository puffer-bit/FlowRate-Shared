using Shared.Enums.Coordinator;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>Deletes a role and takes it from every user who has it.</summary>
public record CoordinatorDeleteRoleRequest([property: EntityId] int RoleId) : CoordinatorRequest
{
    public CoordinatorRoleResponse ToResponse(CoordinatorRoleResult result)
        => new(RequestId, result, null);
}
