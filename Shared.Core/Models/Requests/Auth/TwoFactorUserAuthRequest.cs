using System.ComponentModel.DataAnnotations;
using Shared.Enums.Auth;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Auth;
using Shared.Validation;

namespace Shared.Models.Requests.Auth;

public record TwoFactorUserAuthRequest(
    [property: StringLength(Limits.PasswordMaxLength)] string? Password,
    [property: StringLength(Limits.TwoFactorMaxLength), NoControlCharacters] string? TwoFactorCode) : AuthRequest
{
    public TwoFactorUserAuthResponse ToResponse(AuthResult authResult, UserDTO? userDTO = null,
        string? serverMessage = null)
        => new(RequestId, userDTO, authResult, serverMessage);
}