namespace Shared.Models.Responses.SessionActions.Generic;

public record ChangeSessionPermissionsResponse(
    Guid RequestId,
    int SessionId) : GenericSessionResponse(RequestId);
