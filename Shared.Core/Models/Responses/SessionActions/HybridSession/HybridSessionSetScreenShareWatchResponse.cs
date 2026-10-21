using Shared.Enums;
using Shared.Models.Media;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionSetScreenShareWatchResponse(
    Guid RequestId,
    SetScreenShareWatchResult Result,
    int UserId,
    bool IsWatching,
    Inbound? Inbound,
    Inbound? AudioInbound = null) : HybridSessionResponse(RequestId);
