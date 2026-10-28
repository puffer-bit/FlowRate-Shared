namespace Shared.Models.Responses.Coordinator;

public record UnregisterPushTokenResponse(Guid RequestId) : CoordinatorResponse(RequestId);
