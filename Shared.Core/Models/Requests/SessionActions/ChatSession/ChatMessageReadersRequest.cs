using Shared.Models.DataTransferObjects.ChatSession;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>
/// Asks who has read a message of the sender in a direct chat or a group, and when. Only the author of the
/// message may ask (<see cref="Enums.ErrorCode.NotMessageAuthor"/>); server channels answer
/// <see cref="Enums.ErrorCode.WrongSessionType"/>.
/// </summary>
public record ChatMessageReadersRequest(
    [property: EntityId] int ChatId,
    [property: EntityId] int MessageId
) : ChatSessionRequest
{
    public ChatMessageReadersResponse ToResponse(List<ChatMessageReaderDTO> readers)
        => new(RequestId, readers);
}
