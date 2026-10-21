using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.Generic;

public record RejectUserResponse(
    Guid RequestId,
    int SessionId,
    int UserTargetId,
    SessionType SessionType,
    RejectUserSessionResult Result ) : GenericSessionResponse(RequestId);
