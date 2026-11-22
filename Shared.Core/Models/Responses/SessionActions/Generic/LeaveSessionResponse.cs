using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.Generic;

public record LeaveSessionResponse(
    Guid RequestId,
    int SessionId) : GenericSessionResponse(RequestId);
