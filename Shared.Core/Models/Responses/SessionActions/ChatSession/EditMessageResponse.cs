namespace Shared.Models.Responses.SessionActions.ChatSession;

public record EditMessageResponse(Guid RequestId) : ChatSessionResponse(RequestId);
