namespace Shared.Models.Responses.Coordinator.Security;

/// <param name="OptionsJson">
/// WebAuthn PublicKeyCredentialCreationOptionsJSON, binary values in base64url, for
/// PublicKeyCredential.parseCreationOptionsFromJSON or a platform API. Good for one answer and five minutes.
/// </param>
public record UserPasskeyOptionsResponse(
    Guid RequestId,
    string OptionsJson) : CoordinatorResponse(RequestId);
