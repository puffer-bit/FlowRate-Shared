using Shared.Enums.User;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Notifications.CoordinatorInfo;

public record CoordinatorUserListUpdatedNotification(IList<UserDTO> Users)
    : CoordinatorUpdatedNotification;

public record CoordinatorUserConnectedNotification(UserDTO User)
    : CoordinatorUpdatedNotification;

public record CoordinatorUserUpdatedNotification(UserDTO User)
    : CoordinatorUpdatedNotification;

public record CoordinatorUserDisconnectedNotification(int UserId, string? Reason = null)
    : CoordinatorUpdatedNotification;

public record CoordinatorUserKickedNotification(int UserId, string? Reason = null)
    : CoordinatorUpdatedNotification;

/// <summary>
/// Sent to the connections of a session that has just ended, right before they are closed. The client drops the
/// session token and does not reconnect on its own.
/// </summary>
public record CoordinatorUserSessionEndedNotification(UserSessionEndReason Reason)
    : CoordinatorUpdatedNotification;

public record CoordinatorUserBannedNotification(int UserId, string? Reason = null)
    : CoordinatorUpdatedNotification;

/// <summary>The user was deleted; a client drops them from its lists.</summary>
public record CoordinatorUserDeletedNotification(int UserId)
    : CoordinatorUpdatedNotification;

/// <summary>How many users are connected now; sent whenever one connects or disconnects.</summary>
public record CoordinatorOnlineUsersChangedNotification(int ConnectedUsersCount)
    : CoordinatorUpdatedNotification;
