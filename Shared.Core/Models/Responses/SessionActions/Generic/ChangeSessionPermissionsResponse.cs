using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.Generic;

public record ChangeSessionPermissionsResponse(
    Guid RequestId,
    int SessionId,
    ChangeSessionPermissionsResult Result) : GenericSessionResponse(RequestId);
