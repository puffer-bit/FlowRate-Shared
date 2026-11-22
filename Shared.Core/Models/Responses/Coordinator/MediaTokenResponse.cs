namespace Shared.Models.Responses.Coordinator;

public record MediaTokenResponse(
    Guid RequestId,
    string Token,
    DateTimeOffset ExpiresAt) : CoordinatorResponse(RequestId);
