using Shared.Models.DataTransferObjects;
using Shared.Models.DataTransferObjects.ChatSession;
using Shared.Models.DataTransferObjects.ChatSession.Messages;

namespace Shared.Models.Notifications.SessionInfo;

public abstract record ChatSessionUpdatedNotification : Notification;

public record ChatSessionMessageAddedNotification(int ChatId, ChatMessageDTO Message)
    : ChatSessionUpdatedNotification;

public record ChatSessionMessageEditedNotification(int ChatId, ChatMessageDTO Message)
    : ChatSessionUpdatedNotification;

public record ChatSessionMessageDeletedNotification(int ChatId, int MessageId, long Revision)
    : ChatSessionUpdatedNotification;

public record ChatSessionUserTypingNotification(int ChatId, int UserId)
    : ChatSessionUpdatedNotification;

/// <summary>
/// Sent to every connection of the user whose read position moved, so all devices show the same
/// unread counts.
/// </summary>
public record ChatReadStateChangedNotification(ChatReadStateDTO State)
    : ChatSessionUpdatedNotification;
