using Shared.Enums.User;

namespace Shared.Models.Responses.Coordinator.Security;

/// <param name="UserIdentity">The new identity to log in with; null unless the result is NoError.</param>
public record UserIdentityResetResponse(
    Guid RequestId,
    UserSecurityResult Result,
    string? UserIdentity) : CoordinatorResponse(RequestId);
