using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Models.Media.Transport;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

public record JoinSessionRequest(
    [property: EntityId] int SessionId,
    [property: DefinedEnum] SessionType SessionType
) : GenericSessionRequest
{
    public JoinSessionResponse ToResponse(JoinSessionResult result, TransportData? transportData = null)
        => new(RequestId, SessionId, SessionType, result, transportData);
}
