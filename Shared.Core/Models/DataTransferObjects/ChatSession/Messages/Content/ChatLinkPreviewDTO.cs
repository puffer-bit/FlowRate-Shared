namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Content;

/// <summary>
/// What a web page says about itself, gathered by the server from its metadata (Open Graph, Twitter
/// cards, the title). A preview never changes once made: a page fetched again gets a new
/// <see cref="Id"/>, so a client may keep what it has fetched by that id.
/// </summary>
public class ChatLinkPreviewDTO
{
    /// <summary>The image is served at <c>{MediaEndpoints.LinkPreviewImages}/{Id}</c>.</summary>
    public required int Id { get; init; }

    /// <summary>The address the preview was made for, as found in the text.</summary>
    public required string Url { get; init; }

    public string? SiteName { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }

    public bool HasImage { get; init; }
    public int ImageWidth { get; init; }
    public int ImageHeight { get; init; }

    /// <summary>
    /// The image is wide and meant to be shown in full under the text; otherwise it is a small
    /// picture beside it.
    /// </summary>
    public bool IsLargeImage { get; init; }
}
