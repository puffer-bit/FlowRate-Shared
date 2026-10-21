using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record EditMessageResponse(
    Guid RequestId,
    EditMessageResult Result) : ChatSessionResponse(RequestId);
