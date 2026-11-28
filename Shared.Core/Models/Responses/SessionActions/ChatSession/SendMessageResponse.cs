using Shared.Models.DataTransferObjects.ChatSession.Messages;

namespace Shared.Models.Responses.SessionActions.ChatSession;

/// <param name="RequestId">The request answered.</param>
public record SendMessageResponse(Guid RequestId) : ChatSessionResponse(RequestId)
{
    /// <summary>
    /// The stored message, as the members get it; the one stored before when the request repeated a
    /// <c>ClientMessageId</c>. Null from older servers.
    /// </summary>
    public ChatMessageDTO? Message { get; init; }
}
