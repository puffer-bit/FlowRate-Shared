using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionSetMicrophoneMuteRequest(
    [property: EntityId] int SessionId,
    bool IsMuted) : HybridSessionRequest
{
    public HybridSessionSetMicrophoneMuteResponse ToResponse(SetMicrophoneMuteResult result)
        => new (RequestId, result, IsMuted);
}
