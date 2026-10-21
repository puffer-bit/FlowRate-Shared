namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Content;

public class ChatVideoContentDTO
{
    public required int Id { get; init; }
    public required string FileName { get; init; }
    public string? Thumbnail { get; set; }
    public string? Description { get; set; }
    public required long Size { get; init; }
    public required string MimeType { get; init; }

    public required int Width { get; init; }
    public required int Height { get; init; }
    public required TimeSpan Duration { get; init; }

    public ChatContentType Type => ChatContentType.Video;
}
