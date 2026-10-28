using System.ComponentModel.DataAnnotations;
using Shared.Enums.Auth;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Auth;
using Shared.Validation;

namespace Shared.Models.Requests.Auth;

/// <summary>
/// The first login stage. A <paramref name="SessionToken"/> the server still knows logs the device in at once, without
/// a second stage; otherwise the server goes on with <paramref name="UserIdentity"/>, and a null identity makes a new
/// user. Every successful login answers with the next token of the session, the one to send next time.
/// </summary>
public record UserAuthRequest(
    [property: StringLength(Limits.PasswordMaxLength)] string? ServerPassword,
    [property: StringLength(Limits.UserIdentityMaxLength), NoControlCharacters] string? UserIdentity,
    [property: StringLength(Limits.SessionTokenMaxLength), NoControlCharacters] string? SessionToken = null,
    DeviceInfoDTO? Device = null) : AuthRequest
{
    /// <param name="userIdentity">The identity of a new user; null when the client logged in with its own.</param>
    public UserAuthResponse ToResponse(UserDTO user, string sessionToken, string? userIdentity = null)
        => new(RequestId, AuthResult.Success, user, userIdentity, sessionToken);

    public UserAuthResponse ToSecondStageResponse(List<SecondStageAuthType> secondStages,
        FIDO2LoginOptionsDTO? fido2LoginOptions = null)
        => new(RequestId, AuthResult.SecondStageRequired, SecondStages: secondStages, FIDO2LoginOptions: fido2LoginOptions);
}
