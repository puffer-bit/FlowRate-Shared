using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionSetMicrophoneMuteRequest(
    [property: EntityId] int SessionId,
    bool IsMuted) : HybridSessionRequest
{
    public HybridSessionSetMicrophoneMuteResponse ToResponse()
        => new (RequestId, IsMuted);
}
