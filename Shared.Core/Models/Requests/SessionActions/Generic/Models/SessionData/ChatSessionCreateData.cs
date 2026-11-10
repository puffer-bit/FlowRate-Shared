using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic.Models.SessionData;

public record ChatSessionCreateData(
    [property: StringLength(Limits.SessionDescriptionMaxLength), NoControlCharacters(allowLineBreaks: true)] string? Description = null,
    [property: StringLength(Limits.UrlMaxLength), HttpsUrl] string? IconPath = null,
    bool IsEncrypted = false,
    bool IsAttachmentsAllowed = true,
    bool IsVoiceMessagesAllowed = true,
    bool IsVideoMessagesAllowed = true,
    bool IsDelayedMessagesAllowed = true,
    [property: DefinedEnum] ChatKind Kind = ChatKind.ServerChannel,
    [property: ItemCount(0, 100)] int[]? MemberIds = null
) : CreateSessionData, IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SessionType != SessionType.Chat)
            yield return new ValidationResult("Chat data must create a text chat.", [nameof(SessionType)]);
        if (MemberIds is { Length: > 100 } || MemberIds?.Any(id => id <= 0) == true)
            yield return new ValidationResult("Invalid chat membership.", [nameof(MemberIds)]);
    }

    public override SessionType SessionType { get; init; } = SessionType.Chat;
}
