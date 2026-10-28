using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.SessionActions.Generic;

public record SessionInfoResponse(
    Guid RequestId,
    IDictionary<int, SessionDTO> Sessions,
    SessionLayoutDTO? Layout = null) : GenericSessionResponse(RequestId);
