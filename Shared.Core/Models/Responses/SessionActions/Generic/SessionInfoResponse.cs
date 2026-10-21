using Shared.Enums;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.SessionActions.Generic;

public record SessionInfoResponse(
    Guid RequestId,
    IDictionary<int, SessionDTO> SessionsList,
    SessionRequestResult Result,
    SessionLayoutDTO? Layout = null) : GenericSessionResponse(RequestId);
