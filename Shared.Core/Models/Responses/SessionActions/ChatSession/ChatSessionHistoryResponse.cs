using Shared.Models.DataTransferObjects.ChatSession.Messages;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatSessionHistoryResponse(
    Guid RequestId,
    List<ChatMessageDTO> Messages) : ChatSessionResponse(RequestId);
