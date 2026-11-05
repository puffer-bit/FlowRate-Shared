using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>Sets the authenticated user's reaction; repeating the same state is harmless.</summary>
public record SetMessageReactionRequest(
    [property: EntityId] int ChatId,
    [property: EntityId] int MessageId,
    [property: Required, NotBlank, StringLength(32), NoControlCharacters] string Emoji,
    bool IsAdded
) : ChatSessionRequest
{
    public SetMessageReactionResponse ToResponse() => new(RequestId);
}
