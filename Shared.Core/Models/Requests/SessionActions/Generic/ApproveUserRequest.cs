using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

public record ApproveUserRequest(
    [property: EntityId] int SessionId,
    [property: EntityId] int UserTargetId,
    [property: DefinedEnum] SessionType SessionType
) : GenericSessionRequest
{
    public ApproveUserResponse ToResponse()
        => new(RequestId, SessionId, UserTargetId, SessionType);
}
