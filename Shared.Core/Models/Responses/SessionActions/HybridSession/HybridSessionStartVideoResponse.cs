using Shared.Enums;
using Shared.Models.Media;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStartVideoResponse(
    Guid RequestId,
    StartVideoShareResult Result,
    Outbound? Outbound) : HybridSessionResponse(RequestId);
