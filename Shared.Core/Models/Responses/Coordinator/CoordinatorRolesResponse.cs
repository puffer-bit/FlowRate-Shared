using Shared.Enums;
using Shared.Enums.Coordinator;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

/// <param name="Roles">All roles, lowest first.</param>
/// <param name="Permissions">What the sender may do; only filled in answer to the roles request.</param>
public record CoordinatorRolesResponse(
    Guid RequestId,
    CoordinatorRoleResult Result,
    IList<RoleDTO>? Roles,
    Permission Permissions) : CoordinatorResponse(RequestId);
