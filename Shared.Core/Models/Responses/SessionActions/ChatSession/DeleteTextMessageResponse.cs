namespace Shared.Models.Responses.SessionActions.ChatSession;

public record DeleteTextMessageResponse(Guid RequestId) : ChatSessionResponse(RequestId);
