using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.Generic;

public record ApproveUserResponse(
    Guid RequestId,
    int SessionId,
    int TargetUserId) : GenericSessionResponse(RequestId);
