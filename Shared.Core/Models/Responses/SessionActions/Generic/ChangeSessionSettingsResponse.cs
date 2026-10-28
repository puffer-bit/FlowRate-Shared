namespace Shared.Models.Responses.SessionActions.Generic;

public record ChangeSessionSettingsResponse(
    Guid RequestId,
    int SessionId) : GenericSessionResponse(RequestId);
