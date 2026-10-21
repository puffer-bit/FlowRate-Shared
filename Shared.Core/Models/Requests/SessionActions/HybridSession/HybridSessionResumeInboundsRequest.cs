using Shared.Enums;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionResumeInboundsRequest(
    [property: EntityId] int SessionId,
    [property: ItemCount(1, Limits.ResumedInboundsMaxCount)] List<string> InboundIds) : HybridSessionRequest
{
    public HybridSessionResumeInboundsResponse ToResponse(ResumeInboundsResult result)
        => new (RequestId, result);
}
