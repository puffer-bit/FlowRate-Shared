using System.ComponentModel.DataAnnotations;
using Shared.Enums.Auth;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Auth;
using Shared.Validation;

namespace Shared.Models.Requests.Auth;

/// <summary>
/// Logs the connection in. The server password is asked of every login while the coordinator has one. A login by
/// username goes on with <see cref="LoginProofRequest"/> or <see cref="LoginPasskeyRequest"/>; every other method
/// answers at once.
/// </summary>
public record LoginRequest(
    [property: StringLength(Limits.PasswordMaxLength)] string? ServerPassword,
    [property: Required] LoginMethodDTO Method,
    DeviceInfoDTO? Device = null) : AuthRequest
{
    /// <param name="deviceId">The device the client logs in with from now on.</param>
    public LoginResponse ToResponse(UserDTO user, Guid deviceId) => new(RequestId, AuthResult.Success, user, deviceId);

    public LoginResponse ToSecondStageResponse(List<LoginFactor> factors, FIDO2LoginOptionsDTO? passkeyOptions = null)
        => new(RequestId, AuthResult.SecondStageRequired, Factors: factors, PasskeyOptions: passkeyOptions);
}
