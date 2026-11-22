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
    public DateTimeOffset? ScheduledAt { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }

    public ChatTextContentDTO? TextContent { get; set; }
    [EntityId]
    public int? ReplyToMessageId { get; init; }

    /// <summary>Every attachment, images as <see cref="NewChatImageModel"/>.</summary>
    [Required, ItemCount(Limits.ChatAttachmentsMaxCount)]
    public List<NewChatFileModel> Files { get; init; } = new();

    /// <summary>The message goes without a preview of its link, as the author closed it in the composer.</summary>
    public bool IsLinkPreviewDisabled { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (TextContent == null && Files.Count == 0)
            yield return new ValidationResult("A message needs text or an attachment.");

        if (Files.Any(file => file is NewChatImageModel { PreviewUploadId: null }))
            yield return new ValidationResult("An image is sent with its reduced copy.", [nameof(Files)]);

        var uploads = Files.SelectMany(file => file.PreviewUploadId is { } preview ? new[] { file.UploadId, preview } : [file.UploadId])
            .ToList();
        if (uploads.Distinct(StringComparer.OrdinalIgnoreCase).Count() != uploads.Count)
            yield return new ValidationResult("An upload is attached once.", [nameof(Files)]);

        // Clock skew between client and server is tolerated for a few minutes
        var now = DateTimeOffset.UtcNow;
        var skew = TimeSpan.FromMinutes(5);

        if (ScheduledAt is { } delayed
                 && (delayed.ToUniversalTime() < now - skew || delayed.ToUniversalTime() > now + Limits.ChatMessageMaxDelay))
            yield return new ValidationResult("ScheduledAt is outside the allowed range.", [nameof(ScheduledAt)]);

        if (ExpiresAt is { } dispose
                 && (dispose.ToUniversalTime() < now - skew || dispose.ToUniversalTime() > now + Limits.ChatMessageMaxLifetime))
            yield return new ValidationResult("ExpiresAt is outside the allowed range.", [nameof(ExpiresAt)]);
    }
}
