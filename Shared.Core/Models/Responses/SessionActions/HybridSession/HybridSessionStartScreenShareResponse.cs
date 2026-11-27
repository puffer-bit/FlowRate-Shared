using Shared.Models.Media;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStartScreenShareResponse(
    Guid RequestId,
    Outbound Outbound,
    Outbound? AudioOutbound = null) : HybridSessionResponse(RequestId);
