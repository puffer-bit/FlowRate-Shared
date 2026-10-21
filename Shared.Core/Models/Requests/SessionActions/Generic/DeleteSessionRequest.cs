using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

public record DeleteSessionRequest(
    [property: EntityId] int SessionId
) : GenericSessionRequest
{
    public DeleteSessionResponse ToResponse(DeleteSessionResult result)
        => new(RequestId, result);
}
