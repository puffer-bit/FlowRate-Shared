using Shared.Models.DataTransferObjects.ChatSession;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatMarkReadResponse(
    Guid RequestId,
    ChatReadStateDTO State) : ChatSessionResponse(RequestId);
