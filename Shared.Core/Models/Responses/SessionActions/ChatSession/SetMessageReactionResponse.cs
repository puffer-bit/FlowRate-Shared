namespace Shared.Models.Responses.SessionActions.ChatSession;

public record SetMessageReactionResponse(Guid RequestId) : ChatSessionResponse(RequestId);
