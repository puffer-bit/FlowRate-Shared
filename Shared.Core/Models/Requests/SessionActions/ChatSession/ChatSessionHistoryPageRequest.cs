using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.DataTransferObjects.ChatSession.Messages;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>
/// Asks for the messages that precede <paramref name="BeforeMessageId"/>, or for the newest ones
/// when it is null.
/// </summary>
public record ChatSessionHistoryPageRequest(
    [property: EntityId] int ChatId,
    [property: EntityId] int? BeforeMessageId,
    [property: Range(1, Limits.ChatHistoryMaxTake)] int Take
) : ChatSessionRequest
{
    public ChatSessionHistoryPageResponse ToResponse(FetchMessagesResult result, List<ChatMessageDTO>? messages,
        long currentRevision)
        => new(RequestId, result, messages, currentRevision);
}
