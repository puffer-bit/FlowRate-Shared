using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Auth;

/// <param name="SessionToken">The token to log in with next time.</param>
public record FIDO2UserAuthResponse(
    Guid RequestId,
    UserDTO User,
    string SessionToken) : AuthResponse(RequestId);
