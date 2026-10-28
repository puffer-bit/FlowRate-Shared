using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionStopVoiceRequest(
    [property: EntityId] int SessionId) : HybridSessionRequest
{
    public HybridSessionStopVoiceResponse ToResponse()
        => new (RequestId);
}
