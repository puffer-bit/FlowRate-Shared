using Shared.Models.Media;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionSetScreenShareWatchResponse(
    Guid RequestId,
    int UserId,
    bool IsWatching,
    Inbound? Inbound,
    Inbound? AudioInbound = null) : HybridSessionResponse(RequestId);
