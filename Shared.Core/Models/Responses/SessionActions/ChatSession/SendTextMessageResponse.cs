using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record SendTextMessageResponse(
    Guid RequestId,
    AddMessageResult Result) : ChatSessionResponse(RequestId);
