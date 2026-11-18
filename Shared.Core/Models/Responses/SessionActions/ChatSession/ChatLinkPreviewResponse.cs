using Shared.Models.DataTransferObjects.ChatSession.Messages.Content;

namespace Shared.Models.Responses.SessionActions.ChatSession;

/// <summary><see cref="Preview"/> is null when the page has nothing to show or could not be fetched.</summary>
public record ChatLinkPreviewResponse(Guid RequestId, ChatLinkPreviewDTO? Preview) : ChatSessionResponse(RequestId);
