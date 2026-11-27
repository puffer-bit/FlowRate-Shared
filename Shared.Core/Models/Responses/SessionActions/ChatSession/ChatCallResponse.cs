using Shared.Models.DataTransferObjects;
namespace Shared.Models.Responses.SessionActions.ChatSession;
public record ChatCallResponse(Guid RequestId, ChatCallDTO Call) : ChatSessionResponse(RequestId);
