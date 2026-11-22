using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession.Models;

/// <summary>
/// A file of a new message. Its bytes are uploaded before the message is sent; the size and the type of the file
/// are the ones of the upload. A kind the server does not know is taken as a plain file.
/// </summary>
[JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true)]
[JsonDerivedType(typeof(NewChatFileModel), typeDiscriminator: "File")]
[JsonDerivedType(typeof(NewChatImageModel), typeDiscriminator: "Image")]
public class NewChatFileModel
{
    [Required, StringLength(Limits.MediaUploadIdLength * 2), HexBytes(Limits.MediaUploadIdLength)]
    public required string UploadId { get; init; }
    [Required, NotBlank, StringLength(Limits.FileNameMaxLength), NoControlCharacters]
    public required string FileName { get; init; }
    /// <summary>
    /// The reduced copy of an image, or a frame of a video uploaded as
    /// <see cref="Media.MediaUploadMetadata.KindImagePreview"/> and shown in its place until it is played.
    /// Required for an image; of other files only one that plays as a video carries one.
    /// </summary>
    [StringLength(Limits.MediaUploadIdLength * 2), HexBytes(Limits.MediaUploadIdLength)]
    public string? PreviewUploadId { get; init; }
}

/// <summary>An image of a new message, uploaded with its reduced copy as <see cref="NewChatFileModel.PreviewUploadId"/>.</summary>
public class NewChatImageModel : NewChatFileModel
{
    [StringLength(Limits.ChatAttachmentDescriptionMaxLength), NoControlCharacters(allowLineBreaks: true)]
    public string? Description { get; init; }
    [Range(1, Limits.ChatImageMaxSide)]
    public required int Width { get; init; }
    [Range(1, Limits.ChatImageMaxSide)]
    public required int Height { get; init; }
}
