namespace Shared.Models.Responses.Coordinator;

/// <param name="RoleIds">The roles the user has now, without the everyone role.</param>
public record CoordinatorUserRolesResponse(
    Guid RequestId,
    int UserTargetId,
    IList<int> RoleIds) : CoordinatorResponse(RequestId);
