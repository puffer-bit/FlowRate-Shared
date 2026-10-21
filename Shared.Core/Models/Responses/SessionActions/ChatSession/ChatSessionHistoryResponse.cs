using Shared.Enums;
using Shared.Models.DataTransferObjects.ChatSession.Messages;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatSessionHistoryResponse(
    Guid RequestId,
    FetchMessagesResult Result,
    List<ChatMessageDTO>? Messages) : ChatSessionResponse(RequestId);
