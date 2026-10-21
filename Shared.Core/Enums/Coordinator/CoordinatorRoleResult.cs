namespace Shared.Enums.Coordinator;

public enum CoordinatorRoleResult
{
    InternalError = -1,
    NoError = 0,
    InsufficientPermissions = 1,

    /// <summary>The role or the user is not below the sender's highest role.</summary>
    HierarchyViolation = 2,

    /// <summary>The role was changed by someone else after the revision the change was based on.</summary>
    Conflict = 3,
    RoleNotFound = 4,
    UserNotFound = 5,

    /// <summary>The name, color or permissions are not allowed, or the role cannot be changed that way.</summary>
    InvalidRole = 6,
    TooManyRoles = 7,
    TimedOut = 8,
    Rejected = 9
}
