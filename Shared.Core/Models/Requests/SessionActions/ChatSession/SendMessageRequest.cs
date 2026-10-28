using System.ComponentModel.DataAnnotations;
using Shared.Models.Requests.SessionActions.ChatSession.Models;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

public record SendMessageRequest(
    [property: EntityId] int ChatId,
    [property: Required] NewChatMessageModel Message
) : ChatSessionRequest
{
    public SendMessageResponse ToResponse()
        => new(RequestId);
}
