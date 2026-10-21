using System.ComponentModel.DataAnnotations;
using Shared.Enums.Auth;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Auth;
using Shared.Validation;

namespace Shared.Models.Requests.Auth;

public record UserAuthRequest(
    [property: StringLength(Limits.PasswordMaxLength)] string? ServerPassword,
    [property: StringLength(Limits.UserIdentityMaxLength), NoControlCharacters] string? UserIdentity,
    bool IsFIDO2AuthRequested = false) : AuthRequest
{
    public UserAuthResponse ToResponse(AuthResult authResult, string? userIdentity = null, UserDTO? userDTO = null,
        string? serverMessage = null)
        => new(RequestId, userDTO, userIdentity, authResult, serverMessage);
}
