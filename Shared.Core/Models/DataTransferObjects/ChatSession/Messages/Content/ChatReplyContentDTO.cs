namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Content;

public class ChatReplyContentDTO
{
    public required int MessageId { get; init; }
    public int? SenderUserId { get; init; }
    public DateTime SentTime { get; init; }
    public ChatTextContentDTO? TextContent { get; init; }
    public bool IsDeleted { get; set; }
    public ChatContentType Type => ChatContentType.Reply;
}
