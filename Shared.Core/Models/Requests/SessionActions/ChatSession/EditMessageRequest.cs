using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>
/// Replaces the text of a message; only its author may do it. <see cref="IsLinkPreviewDisabled"/> turns
/// the preview of its link off or on again; null leaves it as it was.
/// </summary>
public record EditMessageRequest(
    [property: EntityId] int ChatId,
    [property: EntityId] int MessageId,
    [property: Required, NotBlank, StringLength(Limits.ChatTextMaxLength), NoControlCharacters(allowLineBreaks: true)] string Text,
    bool? IsLinkPreviewDisabled = null
) : ChatSessionRequest
{
    public EditMessageResponse ToResponse()
        => new(RequestId);
}
