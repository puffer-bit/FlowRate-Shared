namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatTypingResponse(Guid RequestId) : ChatSessionResponse(RequestId);
