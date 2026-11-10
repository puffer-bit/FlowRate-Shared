namespace Shared.Models.Responses.SessionActions.Generic;

public record ChangeGroupResponse(Guid RequestId, int SessionId) : GenericSessionResponse(RequestId);
