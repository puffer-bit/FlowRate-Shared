using Shared.Models.Media;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionSetScreenShareWatchResponse(
    Guid RequestId,
    int SharerUserId,
    bool IsWatching,
    Inbound? Inbound,
    Inbound? AudioInbound = null) : HybridSessionResponse(RequestId);
