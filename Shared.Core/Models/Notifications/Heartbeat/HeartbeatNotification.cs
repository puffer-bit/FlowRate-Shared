namespace Shared.Models.Notifications.Heartbeat;

public abstract record HeartbeatNotification : Notification;

public record HeartbeatServerClosingNotification(string CoordinatorId, string? Message = null)
    : HeartbeatNotification;

public record HeartbeatServerRestartingNotification(string CoordinatorId, string? Message = null)
    : HeartbeatNotification;
