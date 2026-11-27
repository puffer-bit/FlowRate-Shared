using Shared.Models.Media.Transport;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionJoinResponse(
    Guid RequestId,
    TransportData? TransportData)
    : HybridSessionResponse(RequestId);
