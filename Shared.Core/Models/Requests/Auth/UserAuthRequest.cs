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
    bool IsFIDO2AuthRequested = false,
    [property: StringLength(Limits.SessionTokenMaxLength), NoControlCharacters] string? SessionToken = null,
    DeviceInfoDTO? Device = null) : AuthRequest
{
    public UserAuthResponse ToResponse(AuthResult authResult, string? userIdentity = null, UserDTO? userDTO = null,
        string? serverMessage = null, string? sessionToken = null)
        => new(RequestId, userDTO, userIdentity, authResult, serverMessage, SessionToken: sessionToken);
}
