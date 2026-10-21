using Shared.Enums;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionStopScreenShareRequest(
    [property: EntityId] int SessionId) : HybridSessionRequest
{
    public HybridSessionStopScreenShareResponse ToResponse(StopScreenShareResult result)
        => new (RequestId, result);
}
