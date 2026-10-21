using Shared.Enums;
using Shared.Models.DataTransferObjects.ChatSession.Messages;

namespace Shared.Models.Responses.SessionActions.ChatSession;

/// <summary>
/// Deleted messages come as tombstones: the id and <c>IsDeleted</c>, without content.
/// With <paramref name="HasMore"/> the next request continues from the highest revision received.
/// With <paramref name="Reset"/> there are no messages and the client reloads its history.
/// </summary>
public record ChatSessionChangesResponse(
    Guid RequestId,
    FetchMessagesResult Result,
    List<ChatMessageDTO>? Messages,
    long CurrentRevision,
    bool HasMore,
    bool Reset) : ChatSessionResponse(RequestId);
