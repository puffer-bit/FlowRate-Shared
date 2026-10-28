using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

public record SessionInfoRequest(
    [property: EntityId] int? SessionId) : GenericSessionRequest
{
    public SessionInfoResponse ToResponse(IDictionary<int, SessionDTO> sessionList,
        SessionLayoutDTO? layout = null)
        => new (RequestId, sessionList, layout);
}
