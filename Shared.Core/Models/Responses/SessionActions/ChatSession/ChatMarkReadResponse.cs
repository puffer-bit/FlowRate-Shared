using Shared.Enums;
using Shared.Models.DataTransferObjects.ChatSession;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatMarkReadResponse(
    Guid RequestId,
    ChatReadResult Result,
    ChatReadStateDTO? State) : ChatSessionResponse(RequestId);
