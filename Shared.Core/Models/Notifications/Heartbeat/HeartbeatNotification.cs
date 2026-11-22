using Shared.Enums;

namespace Shared.Models.Notifications.Heartbeat;

public abstract record HeartbeatNotification : Notification;

/// <summary>
/// The server goes down. With <see cref="At"/> it is a warning, sent when an administrator announces it and to
/// whoever connects until then; without it the server is going down now and the connection closes. A plain stop
/// of the process tells <see cref="ServerRestartReason.Restart"/>, since the server cannot know whether it comes
/// back; a client reconnects on its own for every reason but <see cref="ServerRestartReason.Shutdown"/>.
/// </summary>
/// <param name="Message">What the administrator wrote, if anything.</param>
public record ServerRestartNotification(ServerRestartReason Reason, DateTimeOffset? At = null, string? Message = null)
    : HeartbeatNotification;

/// <summary>The announced restart is called off.</summary>
public record ServerRestartCancelledNotification : HeartbeatNotification;
