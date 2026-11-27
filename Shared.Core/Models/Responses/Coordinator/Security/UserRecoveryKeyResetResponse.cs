namespace Shared.Models.Responses.Coordinator.Security;

public record UserRecoveryKeyResetResponse(Guid RequestId) : CoordinatorResponse(RequestId);
