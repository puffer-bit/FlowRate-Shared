using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects.ChatSession.Messages.Content;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession.Models;

/// <summary>
/// What the sender decides about a new message. The author, the sent time and the state flags are
/// set by the server, and a reply is resolved from <see cref="ReplyToMessageId"/> on the server.
/// </summary>
public class NewChatMessageModel : IValidatableObject
{
    [EntityId]
    public required int ChatId { get; init; }
    public DateTime? DelayedSentTime { get; set; }
    public DateTime? DisposeTime { get; set; }

    public ChatTextContentDTO? TextContent { get; set; }
    [EntityId]
    public int? ReplyToMessageId { get; init; }

    [Required, ItemCount(Limits.ChatAttachmentsMaxCount)]
    public List<NewChatImageModel> Images { get; init; } = new();
    [Required, ItemCount(Limits.ChatAttachmentsMaxCount)]
    public List<ChatVideoContentDTO> Video { get; init; } = new();
    [Required, ItemCount(Limits.ChatAttachmentsMaxCount)]
    public List<ChatFileContentDTO> Files { get; init; } = new();

    public bool IsDelayed { get; set; }
    public bool IsDisposable { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var attachments = Images.Count + Video.Count + Files.Count;

        if (TextContent == null && attachments == 0)
            yield return new ValidationResult("A message needs text or an attachment.");

        if (attachments > Limits.ChatAttachmentsMaxCount)
            yield return new ValidationResult($"A message carries at most {Limits.ChatAttachmentsMaxCount} attachments.");

        // Only images can be uploaded so far
        if (Video.Count + Files.Count > 0)
            yield return new ValidationResult("Video and file attachments are not supported.");

        var uploads = Images.SelectMany(image => new[] { image.UploadId, image.PreviewUploadId }).ToList();
        if (uploads.Distinct(StringComparer.OrdinalIgnoreCase).Count() != uploads.Count)
            yield return new ValidationResult("An upload is attached once.", [nameof(Images)]);

        // Clock skew between client and server is tolerated for a few minutes
        var now = DateTime.UtcNow;
        var skew = TimeSpan.FromMinutes(5);

        if (IsDelayed != DelayedSentTime.HasValue)
            yield return new ValidationResult("IsDelayed and DelayedSentTime must be set together.", [nameof(DelayedSentTime)]);
        else if (DelayedSentTime is { } delayed
                 && (delayed.ToUniversalTime() < now - skew || delayed.ToUniversalTime() > now + Limits.ChatMessageMaxDelay))
            yield return new ValidationResult("DelayedSentTime is outside the allowed range.", [nameof(DelayedSentTime)]);

        if (IsDisposable != DisposeTime.HasValue)
            yield return new ValidationResult("IsDisposable and DisposeTime must be set together.", [nameof(DisposeTime)]);
        else if (DisposeTime is { } dispose
                 && (dispose.ToUniversalTime() < now - skew || dispose.ToUniversalTime() > now + Limits.ChatMessageMaxLifetime))
            yield return new ValidationResult("DisposeTime is outside the allowed range.", [nameof(DisposeTime)]);
    }
}
