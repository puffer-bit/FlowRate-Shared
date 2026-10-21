using System.ComponentModel.DataAnnotations;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession.Models;

/// <summary>
/// An image of a new message. Its bytes are uploaded before the message is sent, the image and its
/// reduced copy as two uploads; the size and the type of the file are the ones of the upload.
/// </summary>
public class NewChatImageModel
{
    [Required, StringLength(Limits.MediaUploadIdLength * 2), HexBytes(Limits.MediaUploadIdLength)]
    public required string UploadId { get; init; }
    [Required, StringLength(Limits.MediaUploadIdLength * 2), HexBytes(Limits.MediaUploadIdLength)]
    public required string PreviewUploadId { get; init; }

    [Required, NotBlank, StringLength(Limits.FileNameMaxLength), NoControlCharacters]
    public required string FileName { get; init; }
    [StringLength(Limits.ChatAttachmentDescriptionMaxLength), NoControlCharacters(allowLineBreaks: true)]
    public string? Description { get; init; }

    [Range(1, Limits.ChatImageMaxSide)]
    public required int Width { get; init; }
    [Range(1, Limits.ChatImageMaxSide)]
    public required int Height { get; init; }
}
