using Shared.Enums.Auth;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Auth;

/// <param name="User">The logged in user; with <see cref="AuthResult.Success"/>.</param>
/// <param name="UserIdentity">The identity of a new user; null when the client logged in with its own.</param>
/// <param name="SessionToken">The token to log in with next time; with <see cref="AuthResult.Success"/>.</param>
/// <param name="SecondStages">
/// What the user can prove in the second stage; with <see cref="AuthResult.SecondStageRequired"/>.
/// </param>
/// <param name="FIDO2LoginOptions">The passkey challenge, when <paramref name="SecondStages"/> offers passkeys.</param>
public record UserAuthResponse(
    Guid RequestId,
    AuthResult AuthResult,
    UserDTO? User = null,
    string? UserIdentity = null,
    string? SessionToken = null,
    List<SecondStageAuthType>? SecondStages = null,
    FIDO2LoginOptionsDTO? FIDO2LoginOptions = null) : AuthResponse(RequestId);
