using Shared.Enums;
using Shared.Models.Media;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionSetScreenShareWatchRequest(
    [property: EntityId] int SessionId,
    [property: EntityId] int UserId,
    bool IsWatching) : HybridSessionRequest
{
    public HybridSessionSetScreenShareWatchResponse ToResponse(SetScreenShareWatchResult result, Inbound? inbound = null,
        Inbound? audioInbound = null)
        => new (RequestId, result, UserId, IsWatching, inbound, audioInbound);
}
