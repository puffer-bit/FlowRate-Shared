using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionStopVideoRequest(
    [property: EntityId] int SessionId) : HybridSessionRequest
{
    public HybridSessionStopVideoResponse ToResponse()
        => new (RequestId);
}
