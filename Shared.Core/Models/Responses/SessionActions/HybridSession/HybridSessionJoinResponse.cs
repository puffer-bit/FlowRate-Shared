using Shared.Enums;
using Shared.Models.Media.Transport;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionJoinResponse(
    Guid RequestId,
    HybridSessionJoinResult Result,
    TransportData? TransportData)
    : HybridSessionResponse(RequestId);
