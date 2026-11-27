using Shared.Models.Media;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStartVoiceResponse(
    Guid RequestId,
    Outbound Outbound) : HybridSessionResponse(RequestId);
