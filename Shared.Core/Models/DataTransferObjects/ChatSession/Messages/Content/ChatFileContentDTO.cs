namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Content;

public class ChatFileContentDTO
{
    public required int Id { get; init; }
    public required string FileName { get; init; }
    public required long Size { get; init; }
    public required string MimeType { get; init; }

    /// <summary>Set for a file that plays as a video; null for other files and for those sent before.</summary>
    public ChatFileVideoDTO? Video { get; init; }

    public ChatContentType Type => ChatContentType.File;
}
