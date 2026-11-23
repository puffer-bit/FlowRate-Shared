using Shared.Enums.Auth;
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
/// Sent to a connected device that has just been removed from the account, right before it is closed. The client drops
/// the device key and does not reconnect on its own.
/// </summary>
public record AccountDeviceEndedNotification(DeviceEndReason Reason)
    : CoordinatorUpdatedNotification;

/// <summary>
/// Sent to the other connected devices of a user when a device was added to the account, so the user notices a login
/// that was not theirs.
/// </summary>
public record AccountDeviceAddedNotification(AccountDeviceDTO Device)
    : CoordinatorUpdatedNotification;

/// <summary>A device of the account approved the new device waiting on this connection; it is logged in now.</summary>
public record DeviceLinkCompletedNotification(UserDTO User, Guid DeviceId)
    : CoordinatorUpdatedNotification;

/// <summary>A device of the account turned away the new device waiting on this connection.</summary>
public record DeviceLinkDeclinedNotification
    : CoordinatorUpdatedNotification;

public record CoordinatorUserBannedNotification(int UserId, string? Reason = null)
    : CoordinatorUpdatedNotification;

/// <summary>The user was deleted; a client drops them from its lists.</summary>
public record CoordinatorUserDeletedNotification(int UserId)
    : CoordinatorUpdatedNotification;

/// <summary>How many users are connected now; sent whenever one connects or disconnects.</summary>
public record CoordinatorOnlineUsersChangedNotification(int ConnectedUsersCount)
    : CoordinatorUpdatedNotification;
