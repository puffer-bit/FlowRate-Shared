namespace Shared.Models.Responses.SessionActions.Generic;

public record ChangeHybridSessionSettingsResponse(
    Guid RequestId,
    int SessionId) : GenericSessionResponse(RequestId);
