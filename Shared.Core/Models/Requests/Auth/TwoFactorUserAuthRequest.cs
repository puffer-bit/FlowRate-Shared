using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Auth;
using Shared.Validation;

namespace Shared.Models.Requests.Auth;

public record TwoFactorUserAuthRequest(
    [property: StringLength(Limits.PasswordMaxLength)] string? Password,
    [property: StringLength(Limits.TwoFactorMaxLength), NoControlCharacters] string? TwoFactorCode) : AuthRequest
{
    public TwoFactorUserAuthResponse ToResponse(UserDTO user, string sessionToken)
        => new(RequestId, user, sessionToken);
}