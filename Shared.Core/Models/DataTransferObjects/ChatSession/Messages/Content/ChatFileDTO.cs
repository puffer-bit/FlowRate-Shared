using System.Text.Json.Serialization;

namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Content;

/// <summary>
/// An attachment of a message. A plain file is only downloaded, from <c>{MediaEndpoints.Files}/{Id}</c>; a kind
/// that is shown in the chat is a derived type with what it is shown by. A reader that does not know a kind
/// (a newer one) reads it as a plain file instead of failing the message, so a new kind needs no protocol version.
/// </summary>
[JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true)]
[JsonDerivedType(typeof(ChatFileDTO), typeDiscriminator: "File")]
[JsonDerivedType(typeof(ChatImageFileDTO), typeDiscriminator: "Image")]
[JsonDerivedType(typeof(ChatVideoFileDTO), typeDiscriminator: "Video")]
public class ChatFileDTO
{
    public required int Id { get; init; }
    public required string FileName { get; init; }
    public required long Size { get; init; }
    public required string MimeType { get; init; }
}

/// <summary>
/// An image, served at <c>{MediaEndpoints.Images}/{Id}</c> and its reduced copy with <c>?variant=preview</c>.
/// </summary>
public class ChatImageFileDTO : ChatFileDTO
{
    public required int Width { get; init; }
    public required int Height { get; init; }
    /// <summary>A tiny blurred copy shown while the image loads; null when there is none.</summary>
    public string? Thumbnail { get; init; }
    public string? Description { get; init; }
}

/// <summary>
/// A file that plays as a video, so that it can be shown before any of it is downloaded. The frame it is shown with
/// is served at <c>{MediaEndpoints.Files}/{Id}?variant=preview</c>.
/// </summary>
public class ChatVideoFileDTO : ChatFileDTO
{
    /// <summary>The sides of the picture as shown, or of its frame when the file says nothing; 0 when unknown.</summary>
    public int Width { get; init; }
    public int Height { get; init; }
    /// <summary>The length of the video, read by the server from its index; null when unknown.</summary>
    public TimeSpan? Duration { get; init; }
    /// <summary>The sender gave a frame of the video, which the server keeps beside the file.</summary>
    public bool HasPreview { get; init; }
}
