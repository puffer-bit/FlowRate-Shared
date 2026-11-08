using Shared.Models.DataTransferObjects.ChatSession.Messages;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatHistoryWindowResponse(Guid RequestId, List<ChatMessageDTO> Messages, long CurrentRevision, bool HasOlder, bool HasNewer) : ChatSessionResponse(RequestId);
