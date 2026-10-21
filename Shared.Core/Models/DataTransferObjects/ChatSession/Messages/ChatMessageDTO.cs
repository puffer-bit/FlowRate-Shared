using Shared.Models.DataTransferObjects.ChatSession.Messages.Content;
using Shared.Models.DataTransferObjects.ChatSession.Messages.Reactions;

namespace Shared.Models.DataTransferObjects.ChatSession.Messages;

public class ChatMessageDTO
{
    public required int Id { get; init; }
    public required int ChatId { get; init; }
    public long Revision { get; set; }
    public required int SenderUserId { get; init; }
    public required DateTime SentTime { get; init; }
    public DateTime? DelayedSentTime { get; set; }
    public DateTime? DisposeTime { get; set; }
    public DateTime? EditTime { get; set; }

    public required ChatTextContentDTO TextContent { get; set; }
    public required ChatReplyContentDTO ReplyContent { get; init; }

    public required List<ChatImageContentDTO> Images { get; init; }
    public required List<ChatVideoContentDTO> Video { get; init; }
    public required List<ChatFileContentDTO> Files { get; init; }

    public required List<ChatReactionDTO> Reactions { get; init; }
    public required List<string> UsersIds { get; init; }

    // Read by the server from the mention tokens of the text
    public List<int> MentionedUserIds { get; set; } = [];
    public bool MentionsEveryone { get; set; }
    public bool MentionsHere { get; set; }

    public bool IsDelayed { get; set; }
    public bool IsEdited { get; set; }
    public bool IsReposted { get; set; }
    public bool IsDelivered { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsReadOnly { get; set; }
    public bool IsSystem { get; set; }
    public bool IsDisposable { get; set; }
}

public enum ChatContentType
{
    File,
    Text,
    Image,
    Video,
    Reply
}
