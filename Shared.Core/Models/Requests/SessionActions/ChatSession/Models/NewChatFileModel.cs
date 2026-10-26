using System.ComponentModel.DataAnnotations;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession.Models;

/// <summary>
/// A file of a new message that is not shown, only downloaded. Its bytes are uploaded before the message
/// is sent; the size and the type of the file are the ones of the upload.
/// </summary>
public class NewChatFileModel
{
    [Required, StringLength(Limits.MediaUploadIdLength * 2), HexBytes(Limits.MediaUploadIdLength)]
    public required string UploadId { get; init; }

    [Required, NotBlank, StringLength(Limits.FileNameMaxLength), NoControlCharacters]
    public required string FileName { get; init; }
}
