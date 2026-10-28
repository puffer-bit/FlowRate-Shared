namespace Shared.Models.Responses.SessionActions.ChatSession;

public record SendTextMessageResponse(Guid RequestId) : ChatSessionResponse(RequestId);
