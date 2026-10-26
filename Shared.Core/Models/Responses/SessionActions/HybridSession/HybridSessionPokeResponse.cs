using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionPokeResponse(
    Guid RequestId,
    PokeResult Result,
    int UserTargetId) : HybridSessionResponse(RequestId);
