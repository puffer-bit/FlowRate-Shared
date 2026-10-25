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

    /// <summary>See a session. The server does not send a session to a user who may not see it.</summary>
    ViewSessions = 1UL << 11,

    /// <summary>Write, edit and delete one's own messages in a chat.</summary>
    SendMessages = 1UL << 12,

    /// <summary>Attach files and images to messages.</summary>
    AttachFiles = 1UL << 13,

    /// <summary>Join a hybrid session.</summary>
    JoinSessions = 1UL << 14,

    /// <summary>Send voice in a hybrid session.</summary>
    Speak = 1UL << 15,

    /// <summary>Send video and share the screen in a hybrid session.</summary>
    ShareVideo = 1UL << 16,

    /// <summary>Poke other peers of a hybrid session.</summary>
    PokeMembers = 1UL << 17,

    All = Administrator | ViewCoordinatorSettings | ManageCoordinatorSettings | ManageRoles | CreateSessions
          | ManageSessions | KickMembers | BanMembers | ApproveMembers | ManageMessages | ManageUsers
          | ViewSessions | SendMessages | AttachFiles | JoinSessions | Speak | ShareVideo | PokeMembers,

    /// <summary>What the everyone role has on a new coordinator, and what it gains on an older one.</summary>
    Default = ViewSessions | SendMessages | AttachFiles | JoinSessions | Speak | ShareVideo | PokeMembers,

    /// <summary>What the overrides of a session can allow or deny; the rest holds on the whole coordinator.</summary>
    SessionScoped = ViewSessions | SendMessages | AttachFiles | JoinSessions | Speak | ShareVideo
                    | PokeMembers | ManageMessages
}
