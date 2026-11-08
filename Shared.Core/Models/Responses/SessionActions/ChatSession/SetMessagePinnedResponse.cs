namespace Shared.Models.Responses.SessionActions.ChatSession;

public record SetMessagePinnedResponse(Guid RequestId) : ChatSessionResponse(RequestId);
