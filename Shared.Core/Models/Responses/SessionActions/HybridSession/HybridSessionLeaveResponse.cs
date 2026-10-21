using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionLeaveResponse(
    Guid RequestId,
    HybridSessionLeaveResult Result)
    : HybridSessionResponse(RequestId);
