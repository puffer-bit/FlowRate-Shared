using Shared.Enums;
using Shared.Models.DataTransferObjects.ChatSession;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatReadStatesResponse(
    Guid RequestId,
    ChatReadResult Result,
    List<ChatReadStateDTO>? States) : ChatSessionResponse(RequestId);
