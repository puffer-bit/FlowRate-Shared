using Shared.Models.DataTransferObjects.ChatSession.User;

namespace Shared.Models.DataTransferObjects.ChatSession;

public class ChatSessionDTO : SessionDTO
{
    public required int CreatorUserId { get; set; }
    public int AdminUserId { get; set; }
    public string? Description { get; set; }
    public string? IconPath { get; set; }

    public required Dictionary<int, TextChatUserDTO> Users { get; set; }

    public bool IsReadOnly { get; set; }
    public bool IsArchived { get; set; }
    public bool IsAttachmentsAllowed { get; set; }
    public bool IsVoiceMessagesAllowed { get; set; }
    public bool IsVideoMessagesAllowed { get; set; }
    public bool IsDelayedMessagesAllowed { get; set; }
}
