using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Requests.SessionActions.Generic.Models;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

public record CreateSessionRequest(
    [property: Required] CreateSessionModel CreateSessionModel
) : GenericSessionRequest
{
    public CreateSessionResponse ToResponse(CreateSessionResult result, SessionDTO? sessionDTO = null)
        => new(RequestId, sessionDTO, result);
}
