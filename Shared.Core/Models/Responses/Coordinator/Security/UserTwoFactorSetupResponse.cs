namespace Shared.Models.Responses.Coordinator.Security;

/// <param name="Secret">Base32, for typing into an authenticator app.</param>
/// <param name="Uri">otpauth:// URI, for a QR code.</param>
public record UserTwoFactorSetupResponse(
    Guid RequestId,
    string Secret,
    string Uri) : CoordinatorResponse(RequestId);
