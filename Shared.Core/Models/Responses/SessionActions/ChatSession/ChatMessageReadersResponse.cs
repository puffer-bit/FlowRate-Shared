using Shared.Models.DataTransferObjects.ChatSession;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatMessageReadersResponse(
    Guid RequestId,
    List<ChatMessageReaderDTO> Readers) : ChatSessionResponse(RequestId);
