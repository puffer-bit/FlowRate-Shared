using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Requests.SessionActions.ChatSession.Models;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

public record SendTextMessageRequest(
    [property: EntityId] int ChatId,
    [property: Required] NewChatMessageModel MessageModel
) : ChatSessionRequest
{
    public SendTextMessageResponse ToResponse(AddMessageResult result)
        => new(RequestId, result);
}
