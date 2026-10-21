using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionStopVideoRequest(
    [property: EntityId] int SessionId) : HybridSessionRequest
{
    public HybridSessionStopVideoResponse ToResponse(StopVideoShareResult result)
        => new (RequestId, result);
}
