using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record DeleteTextMessageResponse(
    Guid RequestId,
    DeleteMessageResult Result) : ChatSessionResponse(RequestId);
