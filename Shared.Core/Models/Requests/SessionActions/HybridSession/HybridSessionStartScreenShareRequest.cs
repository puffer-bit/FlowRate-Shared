using Shared.Enums;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Models.Media;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionStartScreenShareRequest(
    [property: EntityId] int SessionId,
    bool WithAudio = false) : HybridSessionRequest
{
    public HybridSessionStartScreenShareResponse ToResponse(StartScreenShareResult result, Outbound? outbound = null,
        Outbound? audioOutbound = null)
        => new (RequestId, result, outbound, audioOutbound);
}
