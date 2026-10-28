using Shared.Models.Media;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStartVideoResponse(
    Guid RequestId,
    Outbound Outbound) : HybridSessionResponse(RequestId);
