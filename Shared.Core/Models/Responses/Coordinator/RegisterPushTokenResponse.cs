using Shared.Enums;
using Shared.Models.Push;

namespace Shared.Models.Responses.Coordinator;

/// <param name="ReplyToken">
/// On success, the bearer token of <see cref="PushEndpoints.Reply"/> for this device. It answers
/// notifications only and is replaced by every registration of the device.
/// </param>
public record RegisterPushTokenResponse(
    Guid RequestId,
    PushTokenResult Result,
    string? ReplyToken = null) : CoordinatorResponse(RequestId);
