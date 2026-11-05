using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>Transient input activity; never stored in chat history.</summary>
public record ChatTypingRequest([property: EntityId] int ChatId, bool IsTyping) : ChatSessionRequest
{
    public ChatTypingResponse ToResponse() => new(RequestId);
}
