using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Models.Media;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionStartVoiceRequest(
    [property: EntityId] int SessionId) : HybridSessionRequest
{
    public HybridSessionStartVoiceResponse ToResponse(StartVoiceShareResult result, Outbound? outbound = null)
        => new (RequestId, result, outbound);
}
