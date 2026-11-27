using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Requests.SessionActions.Generic.Models;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

public record CreateSessionRequest(
    [property: Required] CreateSessionModel CreateSessionModel
) : GenericSessionRequest
{
    public CreateSessionResponse ToResponse(SessionDTO sessionDTO)
        => new(RequestId, sessionDTO);
}
