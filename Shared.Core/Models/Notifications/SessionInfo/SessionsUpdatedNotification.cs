using Shared.Models.DataTransferObjects;
using Shared.Models.DataTransferObjects.ChatSession;

namespace Shared.Models.Notifications.SessionInfo;

public abstract record SessionsUpdatedNotification : Notification;

public record HybridSessionCreatedNotification(HybridSessionDTO Session)
    : SessionsUpdatedNotification;

public record HybridSessionReconfiguredNotification(HybridSessionDTO Session)
    : SessionsUpdatedNotification;

public record HybridSessionDeletedNotification(int SessionId)
    : SessionsUpdatedNotification;

public record ChatSessionCreatedNotification(ChatSessionDTO Session)
    : SessionsUpdatedNotification;

public record ChatSessionReconfiguredNotification(ChatSessionDTO Session)
    : SessionsUpdatedNotification;

public record ChatSessionDeletedNotification(int SessionId)
    : SessionsUpdatedNotification;

/// <summary>The order of the sessions or their categories changed; carries the whole new layout.</summary>
public record SessionLayoutChangedNotification(SessionLayoutDTO Layout)
    : SessionsUpdatedNotification;
