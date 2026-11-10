using Shared.Models.DataTransferObjects;
namespace Shared.Models.Responses.SessionActions.ChatSession;
public record DirectCallResponse(Guid RequestId, DirectCallDTO Call) : ChatSessionResponse(RequestId);
