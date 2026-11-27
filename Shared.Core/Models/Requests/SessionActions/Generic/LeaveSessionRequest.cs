using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

public record LeaveSessionRequest(
    [property: EntityId] int SessionId
) : GenericSessionRequest
{
    public LeaveSessionResponse ToResponse()
        => new(RequestId, SessionId);
}
