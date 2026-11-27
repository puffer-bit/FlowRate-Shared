using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.SessionActions.Generic;

public record CreateSessionResponse(
    Guid RequestId,
    SessionDTO Session) : GenericSessionResponse(RequestId);
