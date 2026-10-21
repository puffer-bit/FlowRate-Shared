using Shared.Enums;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Notifications.CoordinatorInfo;

/// <summary>A role was created, changed, moved or deleted. Carries all roles, lowest first.</summary>
public record CoordinatorRolesChangedNotification(IList<RoleDTO> Roles)
    : CoordinatorUpdatedNotification;

/// <summary>Sent to a user whose permissions changed, with what they may do now.</summary>
public record CoordinatorPermissionsChangedNotification(Permission Permissions)
    : CoordinatorUpdatedNotification;
