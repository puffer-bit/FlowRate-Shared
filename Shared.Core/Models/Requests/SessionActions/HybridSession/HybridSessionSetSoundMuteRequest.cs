using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionSetSoundMuteRequest(
    [property: EntityId] int SessionId,
    bool IsMuted) : HybridSessionRequest
{
    public HybridSessionSetSoundMuteResponse ToResponse()
        => new (RequestId, IsMuted);
}
