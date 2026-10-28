using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Models.Media;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionStartVoiceRequest(
    [property: EntityId] int SessionId) : HybridSessionRequest
{
    public HybridSessionStartVoiceResponse ToResponse(Outbound outbound)
        => new (RequestId, outbound);
}
