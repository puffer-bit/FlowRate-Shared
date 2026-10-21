using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.Generic;

public record BanInSessionResponse(
    Guid RequestId,
    int SessionId,
    int UserTargetId,
    SessionType SessionType,
    BanFromSessionResult Result ) : GenericSessionResponse(RequestId);
