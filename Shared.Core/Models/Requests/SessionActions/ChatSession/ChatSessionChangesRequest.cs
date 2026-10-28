using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects.ChatSession.Messages;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>
/// Asks for the messages of a chat changed after <paramref name="AfterRevision"/>, in their
/// current state and ordered by revision.
/// </summary>
public record ChatSessionChangesRequest(
    [property: EntityId] int ChatId,
    [property: Range(0, long.MaxValue)] long AfterRevision,
    [property: Range(1, Limits.ChatHistoryMaxTake)] int Take
) : ChatSessionRequest
{
    public ChatSessionChangesResponse ToResponse(List<ChatMessageDTO> messages,
        long currentRevision, bool hasMore, bool reset)
        => new(RequestId, messages, currentRevision, hasMore, reset);
}
