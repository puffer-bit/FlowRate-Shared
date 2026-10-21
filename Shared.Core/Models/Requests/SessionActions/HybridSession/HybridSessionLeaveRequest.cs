using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionLeaveRequest(
    [property: EntityId] int SessionId
) : HybridSessionRequest
{
    public HybridSessionLeaveResponse ToResponse(HybridSessionLeaveResult result)
        => new(RequestId, result);
}
