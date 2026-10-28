using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

public record DeleteTextMessageRequest(
    [property: EntityId] int ChatId,
    [property: EntityId] int MessageId
) : ChatSessionRequest
{
    public DeleteTextMessageResponse ToResponse()
        => new(RequestId);
}
