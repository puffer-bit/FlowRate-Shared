using Shared.Enums;
using Shared.Models.Media;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionStartVoiceResponse(
    Guid RequestId,
    StartVoiceShareResult Result,
    Outbound? Outbound) : HybridSessionResponse(RequestId);
