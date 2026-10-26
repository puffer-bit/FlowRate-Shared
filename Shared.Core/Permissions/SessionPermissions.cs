using Shared.Enums;

namespace Shared.Permissions;

/// <summary>What one role allows and denies in one session.</summary>
public readonly record struct PermissionOverride(int RoleId, Permission Allow, Permission Deny);

/// <summary>
/// The permissions of a user in a session. The server and the client both decide by this, so that the
/// interface offers exactly what the server allows.
/// </summary>
public static class SessionPermissions
{
    /// <summary>
    /// Applies the overrides of a session to what the roles of a user allow on the coordinator.
    /// <see cref="Permission.Administrator"/> is never limited. The override of the everyone role applies
    /// first, then those of the user's other roles together; between them an allow beats a deny.
    /// </summary>
    /// <param name="coordinatorPermissions">What the user's roles and the everyone role allow together.</param>
    /// <param name="roleIds">The roles of the user, without the everyone role.</param>
    public static Permission Resolve(Permission coordinatorPermissions, IEnumerable<PermissionOverride> overrides,
        int everyoneRoleId, IReadOnlyCollection<int> roleIds)
    {
        if (coordinatorPermissions.HasFlag(Permission.Administrator))
            return Permission.All;

        var permissions = coordinatorPermissions;
        var allow = Permission.None;
        var deny = Permission.None;
        foreach (var o in overrides)
        {
            if (o.RoleId == everyoneRoleId)
                permissions = (permissions & ~(o.Deny & Permission.SessionScoped)) | (o.Allow & Permission.SessionScoped);
            else if (roleIds.Contains(o.RoleId))
            {
                allow |= o.Allow;
                deny |= o.Deny;
            }
        }

        return (permissions & ~(deny & Permission.SessionScoped)) | (allow & Permission.SessionScoped);
    }
}
