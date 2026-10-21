using Shared.Models.DataTransferObjects;
using Shared.Models.Notifications.SessionInfo;

namespace Shared.Models.Notifications.CoordinatorInfo;

public abstract record CoordinatorUpdatedNotification : Notification;

public record CoordinatorReconfiguredNotification(CoordinatorSessionDTO CoordinatorSessionDTO)
    : CoordinatorUpdatedNotification;
