namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Content;

/// <summary>
/// What the server knows of a file that plays as a video, so that it can be shown before any of it is
/// downloaded. The frame it is shown with is served at <c>{MediaEndpoints.Files}/{id}?variant=preview</c>.
/// </summary>
public class ChatFileVideoDTO
{
    /// <summary>The sides of the picture as shown, or of its frame when the file says nothing; 0 when unknown.</summary>
    public int Width { get; init; }
    public int Height { get; init; }

    /// <summary>The length of the video, read by the server from its index; null when unknown.</summary>
    public TimeSpan? Duration { get; init; }

    /// <summary>The sender gave a frame of the video, which the server keeps beside the file.</summary>
    public bool HasPreview { get; init; }
}
