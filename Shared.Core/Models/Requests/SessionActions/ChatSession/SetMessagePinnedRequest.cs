using Shared.Validation;
using Shared.Models.Responses.SessionActions.ChatSession;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>Sets a shared chat pin; repeating the same state is harmless.</summary>
public record SetMessagePinnedRequest(
    [property: EntityId] int ChatId,
    [property: EntityId] int MessageId,
    bool IsPinned
) : ChatSessionRequest
{
    public SetMessagePinnedResponse ToResponse() => new(RequestId);
}
