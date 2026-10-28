using Shared.Models.DataTransferObjects.ChatSession;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatReadStatesResponse(
    Guid RequestId,
    List<ChatReadStateDTO> States) : ChatSessionResponse(RequestId);
