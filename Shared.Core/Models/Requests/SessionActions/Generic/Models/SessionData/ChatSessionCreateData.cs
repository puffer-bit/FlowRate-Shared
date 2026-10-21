using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic.Models.SessionData;

public record ChatSessionCreateData(
    [property: StringLength(Limits.SessionDescriptionMaxLength), NoControlCharacters(allowLineBreaks: true)] string? Description = null,
    [property: StringLength(Limits.UrlMaxLength), HttpsUrl] string? IconPath = null,
    bool IsEncrypted = false,
    bool IsAttachmentsAllowed = true,
    bool IsVoiceMessageAllowed = true,
    bool IsVideoMessagesAllowed = true,
    bool IsDelayedMessagesAllowed = true
) : CreateSessionData
{
    public override SessionType SessionType { get; init; } = SessionType.Chat;
}
