using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.DataTransferObjects.ChatSession.Messages;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

public record ChatSessionMessageRequest(
    [property: EntityId] int ChatId,
    [property: EntityId] int MessageId
) : ChatSessionRequest
{
    public ChatSessionMessageResponse ToResponse(FetchMessageResult result, ChatMessageDTO? message)
        => new(RequestId, message, result);
}
