namespace Shared.Models.Responses.SessionActions.ChatSession;

public record DeleteMessageResponse(Guid RequestId) : ChatSessionResponse(RequestId);
