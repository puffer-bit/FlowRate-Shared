using Shared.Enums;
using Shared.Models.DataTransferObjects.ChatSession.Messages;

namespace Shared.Models.Responses.SessionActions.ChatSession;

/// <summary>
/// <paramref name="CurrentRevision"/> is the revision of the chat the page is at least as fresh as.
/// </summary>
public record ChatSessionHistoryPageResponse(
    Guid RequestId,
    FetchMessagesResult Result,
    List<ChatMessageDTO>? Messages,
    long CurrentRevision) : ChatSessionResponse(RequestId);
