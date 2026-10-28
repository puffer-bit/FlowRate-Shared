namespace Shared.Models.Responses.Coordinator;

public record MediaTokenResponse(
    Guid RequestId,
    string Token,
    DateTime ExpiresAt) : CoordinatorResponse(RequestId);
