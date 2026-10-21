using Shared.Enums;
using Shared.Models.DataTransferObjects.ChatSession;
using Shared.Models.Responses.SessionActions.ChatSession;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>
/// Asks for the read positions and unread counts of all chats of the sender.
/// </summary>
public record ChatReadStatesRequest : ChatSessionRequest
{
    public ChatReadStatesResponse ToResponse(ChatReadResult result, List<ChatReadStateDTO>? states)
        => new(RequestId, result, states);
}
