using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.Generic;

public record RejectUserResponse(
    Guid RequestId,
    int SessionId,
    int TargetUserId,
    SessionType SessionType) : GenericSessionResponse(RequestId);
