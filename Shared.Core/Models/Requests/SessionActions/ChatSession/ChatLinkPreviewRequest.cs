using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects.ChatSession.Messages.Content;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>
/// Asks for the preview of an address being typed in a chat, to show it above the input before the
/// message goes. The server fetches the page or takes it from its cache; a message sent with the same
/// address then gets the same preview without another fetch.
/// </summary>
public record ChatLinkPreviewRequest(
    [property: EntityId] int ChatId,
    [property: Required, NotBlank, StringLength(Limits.UrlMaxLength), NoControlCharacters] string Url
) : ChatSessionRequest
{
    public ChatLinkPreviewResponse ToResponse(ChatLinkPreviewDTO? preview) => new(RequestId, preview);
}
