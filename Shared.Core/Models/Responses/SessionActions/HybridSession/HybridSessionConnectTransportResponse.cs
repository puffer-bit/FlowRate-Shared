using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionConnectTransportResponse(
    Guid RequestId,
    HybridSessionConnectTransportResult Result) : HybridSessionResponse(RequestId);
