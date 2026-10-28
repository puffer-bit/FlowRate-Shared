using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Auth;
using Shared.Validation;

namespace Shared.Models.Requests.Auth;

public record FIDO2UserAuthRequest(
    FIDO2AssertionDTO AssertionResponse) : AuthRequest
{
    public FIDO2UserAuthResponse ToResponse(UserDTO user, string sessionToken)
        => new(RequestId, user, sessionToken);
}