using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionSetAfkResponse(
    Guid RequestId,
    SetAfkResult Result,
    bool IsAfk) : HybridSessionResponse(RequestId);
