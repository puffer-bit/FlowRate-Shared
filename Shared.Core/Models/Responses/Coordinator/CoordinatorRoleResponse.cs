using Shared.Enums.Coordinator;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

/// <param name="Role">The role as it is now: the saved one, or on a conflict the newer one.</param>
public record CoordinatorRoleResponse(
    Guid RequestId,
    CoordinatorRoleResult Result,
    RoleDTO? Role) : CoordinatorResponse(RequestId);
