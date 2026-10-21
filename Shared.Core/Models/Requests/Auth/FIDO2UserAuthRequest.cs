using System.ComponentModel.DataAnnotations;
using Shared.Enums.Auth;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Auth;
using Shared.Validation;

namespace Shared.Models.Requests.Auth;

public record FIDO2UserAuthRequest(
    FIDO2AssertionDTO AssertionResponse) : AuthRequest
{
    public FIDO2UserAuthResponse ToResponse(AuthResult authResult, UserDTO? userDTO = null,
        string? serverMessage = null)
        => new(RequestId, userDTO, authResult, serverMessage);
}