using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

public record ChangeChatSessionSettingsRequest(
    [property: EntityId] int SessionId,
    [property: Required, NotBlank, StringLength(Limits.SessionNameMaxLength), NoControlCharacters] string Name,
    [property: StringLength(Limits.SessionDescriptionMaxLength), NoControlCharacters(allowLineBreaks: true)] string? Description
) : GenericSessionRequest
{
    public ChangeChatSessionSettingsResponse ToResponse() => new(RequestId, SessionId);
}
