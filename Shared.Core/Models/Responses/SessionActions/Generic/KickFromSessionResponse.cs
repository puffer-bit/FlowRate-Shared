using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.Generic;

public record KickFromSessionResponse(
    Guid RequestId,
    int SessionId,
    int UserTargetId,
    SessionType SessionType,
    LeaveSessionResult Result ) : GenericSessionResponse(RequestId);
