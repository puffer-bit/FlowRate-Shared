using System.ComponentModel.DataAnnotations;
using Shared.Enums.Auth;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Auth;
using Shared.Validation;

namespace Shared.Models.Requests.Auth;

/// <summary>Finishes a login by username with the password and/or the one-time code, each if the user has it.</summary>
public record LoginProofRequest(
    [property: StringLength(Limits.PasswordMaxLength)] string? Password,
    [property: StringLength(Limits.TwoFactorMaxLength), NoControlCharacters] string? TwoFactorCode) : AuthRequest
{
    public LoginResponse ToResponse(UserDTO user, Guid deviceId) => new(RequestId, AuthResult.Success, user, deviceId);
}

/// <summary>Finishes a login by username with a passkey answering the options of the second stage.</summary>
public record LoginPasskeyRequest(
    [property: Required] FIDO2AssertionDTO Assertion) : AuthRequest
{
    public LoginResponse ToResponse(UserDTO user, Guid deviceId) => new(RequestId, AuthResult.Success, user, deviceId);
}
