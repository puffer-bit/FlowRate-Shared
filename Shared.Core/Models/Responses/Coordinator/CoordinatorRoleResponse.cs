using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

/// <param name="Role">The role as it was saved; null once it is deleted.</param>
public record CoordinatorRoleResponse(
    Guid RequestId,
    RoleDTO? Role) : CoordinatorResponse(RequestId);
