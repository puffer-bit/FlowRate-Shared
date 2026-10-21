using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

public record RejectUserRequest(
    [property: EntityId] int SessionId,
    [property: EntityId] int UserTargetId,
    [property: DefinedEnum] SessionType SessionType,
    [property: StringLength(Limits.ReasonMaxLength), NoControlCharacters] string? Reason
) : GenericSessionRequest
{
    public RejectUserResponse ToResponse(RejectUserSessionResult result)
        => new(RequestId, SessionId, UserTargetId, SessionType, result);
}
