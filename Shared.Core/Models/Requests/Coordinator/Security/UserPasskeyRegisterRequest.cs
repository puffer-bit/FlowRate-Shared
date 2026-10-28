using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator.Security;

/// <summary>
/// Registers the passkey made for the last options of the sender. <paramref name="CredentialJson"/> is the
/// WebAuthn RegistrationResponseJSON (what PublicKeyCredential.toJSON() gives), binary values in base64url.
/// </summary>
public record UserPasskeyRegisterRequest(
    [property: StringLength(Limits.PasskeyLabelMaxLength), NoControlCharacters] string? Label,
    [property: Required, StringLength(Limits.Fido2CredentialJsonMaxLength)] string CredentialJson) : CoordinatorRequest
{
    public UserSecurityResponse ToResponse(UserSecurityDTO security)
        => new(RequestId, security);
}
