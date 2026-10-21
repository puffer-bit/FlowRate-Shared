using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>Replaces the text of a message; only its author may do it.</summary>
public record EditTextMessageRequest(
    [property: EntityId] int ChatId,
    [property: EntityId] int MessageId,
    [property: Required, NotBlank, StringLength(Limits.ChatTextMaxLength), NoControlCharacters(allowLineBreaks: true)] string Text
) : ChatSessionRequest
{
    public EditMessageResponse ToResponse(EditMessageResult result)
        => new(RequestId, result);
}
