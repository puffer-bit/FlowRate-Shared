using Shared.Enums.Auth;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Auth;

/// <param name="Nonce">What the device or the recovery key signs, base64url.</param>
/// <param name="CoordinatorId">The coordinator, signed with the nonce.</param>
public record LoginChallengeResponse(
    Guid RequestId,
    string Nonce,
    string CoordinatorId) : AuthResponse(RequestId);

/// <param name="User">The logged in user; with <see cref="AuthResult.Success"/>.</param>
/// <param name="DeviceId">The device to log in with from now on; with <see cref="AuthResult.Success"/>.</param>
/// <param name="Factors">What the user can prove; with <see cref="AuthResult.SecondStageRequired"/>.</param>
/// <param name="PasskeyOptions">The passkey challenge, when <paramref name="Factors"/> offers passkeys.</param>
public record LoginResponse(
    Guid RequestId,
    AuthResult AuthResult,
    UserDTO? User = null,
    Guid? DeviceId = null,
    List<LoginFactor>? Factors = null,
    FIDO2LoginOptionsDTO? PasskeyOptions = null) : AuthResponse(RequestId);

/// <param name="Code">What the new device shows, and a device of the account enters or scans.</param>
/// <param name="ExpiresAt">The code is good until then.</param>
public record DeviceLinkStartResponse(
    Guid RequestId,
    string Code,
    DateTimeOffset ExpiresAt) : AuthResponse(RequestId);
