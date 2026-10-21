using Shared.Enums;

namespace Shared.Models.Responses.Coordinator;

public record MediaTokenResponse(
    Guid RequestId,
    MediaTokenResult Result,
    string? Token,
    DateTime? ExpiresAt) : CoordinatorResponse(RequestId);
