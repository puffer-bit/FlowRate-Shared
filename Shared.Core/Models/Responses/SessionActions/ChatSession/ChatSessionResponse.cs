namespace Shared.Models.Responses.SessionActions.ChatSession;

public abstract record ChatSessionResponse(Guid RequestId) : Response(RequestId);
