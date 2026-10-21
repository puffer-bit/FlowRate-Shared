using Shared.Enums;

namespace Shared.Models.Responses.Coordinator;

public record UnregisterPushTokenResponse(
    Guid RequestId,
    PushTokenResult Result) : CoordinatorResponse(RequestId);
