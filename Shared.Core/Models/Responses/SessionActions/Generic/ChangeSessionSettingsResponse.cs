using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.Generic;

public record ChangeSessionSettingsResponse(
    Guid RequestId,
    int SessionId,
    ChangeSessionSettingsResult Result) : GenericSessionResponse(RequestId);
