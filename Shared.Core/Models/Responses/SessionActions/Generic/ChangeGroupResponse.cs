namespace Shared.Models.Responses.SessionActions.Generic;

public record ChangeGroupResponse(Guid RequestId, int SessionId) : GenericSessionResponse(RequestId);

public record SetGroupAvatarResponse(Guid RequestId, int SessionId) : GenericSessionResponse(RequestId);

public record LeaveGroupResponse(Guid RequestId, int SessionId) : GenericSessionResponse(RequestId);
