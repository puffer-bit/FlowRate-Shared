using Shared.Models.DataTransferObjects.ChatSession.User;

namespace Shared.Models.DataTransferObjects.ChatSession;

public class ChatSessionDTO : SessionDTO
{
    public Shared.Enums.ChatKind Kind { get; set; }
    public int[] MemberIds { get; set; } = [];
    public Messages.ChatMessageDTO? LastMessage { get; set; }

    public required int CreatorUserId { get; set; }
    /// <summary>The owner of a group, who alone changes its members and name; 0 for other chats.</summary>
    public int OwnerUserId { get; set; }
    public string? Description { get; set; }
    /// <summary>
    /// The picture of a group, a path under <see cref="Shared.Models.Media.MediaEndpoints.Avatars"/>;
    /// null shows the first letter of its name. Set with <c>SetGroupAvatarRequest</c>.
    /// </summary>
    public string? AvatarUrl { get; set; }

    public required Dictionary<int, TextChatUserDTO> Users { get; set; }

    public bool IsReadOnly { get; set; }
    public bool IsArchived { get; set; }
    public bool IsAttachmentsAllowed { get; set; }
    public bool IsVoiceMessagesAllowed { get; set; }
    public bool IsVideoMessagesAllowed { get; set; }
    public bool IsDelayedMessagesAllowed { get; set; }
}
