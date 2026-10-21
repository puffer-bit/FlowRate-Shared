namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Content;

public class ChatFileContentDTO
{
    public required int Id { get; init; }
    public required string FileName { get; init; }
    public required long Size { get; init; }
    public required string MimeType { get; init; }

    public ChatContentType Type => ChatContentType.File;
}
