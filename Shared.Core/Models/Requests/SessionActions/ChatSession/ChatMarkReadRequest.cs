using Shared.Models.DataTransferObjects.ChatSession;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>
/// Moves the read position of the sender in a chat up to <paramref name="MessageId"/>.
/// The position never goes back: an older id leaves it as it is.
/// </summary>
public record ChatMarkReadRequest(
    [property: EntityId] int ChatId,
    [property: EntityId] int MessageId
) : ChatSessionRequest
{
    public ChatMarkReadResponse ToResponse(ChatReadStateDTO state)
        => new(RequestId, state);
}
