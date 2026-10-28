using Shared.Models.Media;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionSetScreenShareWatchRequest(
    [property: EntityId] int SessionId,
    [property: EntityId] int UserId,
    bool IsWatching) : HybridSessionRequest
{
    public HybridSessionSetScreenShareWatchResponse ToResponse(Inbound? inbound = null,
        Inbound? audioInbound = null)
        => new (RequestId, UserId, IsWatching, inbound, audioInbound);
}
