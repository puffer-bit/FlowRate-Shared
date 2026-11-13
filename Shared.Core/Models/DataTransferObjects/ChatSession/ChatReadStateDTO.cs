namespace Shared.Models.DataTransferObjects.ChatSession;

/// <summary>
/// Where the user has read a chat up to and what is unread after that position.
/// Unread messages are the ones of other users, not deleted and not system; the counts stop
/// at <see cref="MaxCount"/>, clients show more as "99+".
/// <paramref name="MentionCount"/> counts the unread messages that mention the user by id or
/// with @everyone.
/// <paramref name="PeerReadMessageId"/> is the furthest read position of the other members in a direct
/// chat or a group (0 in server channels and when nobody else has read anything): a message of the user
/// with an id up to it is read by someone.
/// </summary>
public record ChatReadStateDTO(int ChatId, int LastReadMessageId, int UnreadCount, int MentionCount,
    int PeerReadMessageId = 0)
{
    public const int MaxCount = 100;
}
