namespace Shared.Models.Responses.SessionActions.ChatSession;

public record SendMessageResponse(Guid RequestId) : ChatSessionResponse(RequestId);
