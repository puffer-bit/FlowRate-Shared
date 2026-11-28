using Shared.Models.DataTransferObjects.ChatSession.Messages.Content;
using Shared.Models.DataTransferObjects.ChatSession.Messages.Reactions;

namespace Shared.Models.DataTransferObjects.ChatSession.Messages;

public class ChatMessageDTO
{
    public required int Id { get; init; }
    public required int ChatId { get; init; }
    public long Revision { get; set; }
    public required int SenderUserId { get; init; }

    /// <summary>
    /// What the author's client gave the message when sending it (<c>NewChatMessageModel.ClientMessageId</c>);
    /// null for messages of older clients and system messages.
    /// </summary>
    public Guid? ClientMessageId { get; init; }
    public required DateTimeOffset SentAt { get; init; }
    public DateTimeOffset? ScheduledAt { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }
    public DateTimeOffset? EditedAt { get; set; }
    public DateTimeOffset? PinnedAt { get; set; }
    public int? PinnedByUserId { get; set; }

    public required ChatTextContentDTO TextContent { get; set; }
    public required ChatReplyContentDTO ReplyContent { get; init; }

    /// <summary>Every attachment: images, videos and other files, each as its kind.</summary>
    public required List<ChatFileDTO> Files { get; init; }

    /// <summary>
    /// The preview of the first address in the text. It comes a moment after the message, in a change
    /// of it, once the server has fetched the page; null when there is none.
    /// </summary>
    public ChatLinkPreviewDTO? LinkPreview { get; set; }

    /// <summary>The author sent the message without a preview of its link.</summary>
    public bool IsLinkPreviewDisabled { get; set; }

    public required List<ChatReactionDTO> Reactions { get; init; }

    // Read by the server from the mention tokens of the text
    public List<int> MentionedUserIds { get; set; } = [];
    public bool MentionsEveryone { get; set; }
    public bool MentionsHere { get; set; }

    public bool IsDeleted { get; set; }
    public bool IsReadOnly { get; set; }
    public bool IsSystem { get; set; }
    public ChatSystemContent? SystemContent { get; init; }
}
