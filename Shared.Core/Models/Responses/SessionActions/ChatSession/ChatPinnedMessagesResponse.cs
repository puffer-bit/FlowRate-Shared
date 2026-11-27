using Shared.Models.DataTransferObjects.ChatSession.Messages;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatPinnedMessagesResponse(Guid RequestId, List<ChatMessageDTO> Messages, long CurrentRevision) : ChatSessionResponse(RequestId);
