namespace Shared.Models.Responses.SessionActions.Generic;

public record ChangeChatSessionSettingsResponse(Guid RequestId, int SessionId) : GenericSessionResponse(RequestId);
