namespace Shared.Enums;

/// <summary>
/// What a coordinator role allows. A user has the permissions of all their roles together with those of
/// the everyone role; <see cref="Administrator"/> allows everything.
/// </summary>
[Flags]
public enum Permission : ulong
{
    None = 0,

    /// <summary>Allows everything. Role hierarchy still applies.</summary>
    Administrator = 1UL << 0,

    ViewCoordinatorSettings = 1UL << 1,
    ManageCoordinatorSettings = 1UL << 2,

    /// <summary>Create, change, reorder, delete, assign and unassign roles below one's own highest role.</summary>
    ManageRoles = 1UL << 3,

    CreateSessions = 1UL << 4,

    /// <summary>Change and delete any session, and arrange sessions and categories.</summary>
    ManageSessions = 1UL << 5,

    KickMembers = 1UL << 6,
    BanMembers = 1UL << 7,

    /// <summary>Approve or reject users waiting to enter a session.</summary>
    ApproveMembers = 1UL << 8,

    /// <summary>Delete messages of other users.</summary>
    ManageMessages = 1UL << 9,

    /// <summary>Change the name and avatar of other users.</summary>
    ManageUsers = 1UL << 10,

    All = Administrator | ViewCoordinatorSettings | ManageCoordinatorSettings | ManageRoles | CreateSessions
          | ManageSessions | KickMembers | BanMembers | ApproveMembers | ManageMessages | ManageUsers
}
