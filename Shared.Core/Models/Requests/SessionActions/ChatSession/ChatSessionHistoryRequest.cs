using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects.ChatSession.Messages;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

public record ChatSessionHistoryRequest(
    [property: EntityId] int ChatId,
    [property: Range(1, Limits.ChatHistoryMaxTake)] int Take,
    [property: Range(0, int.MaxValue)] int? Skip
) : ChatSessionRequest
{
    public ChatSessionHistoryResponse ToResponse(List<ChatMessageDTO> messages)
        => new(RequestId, messages);
}
