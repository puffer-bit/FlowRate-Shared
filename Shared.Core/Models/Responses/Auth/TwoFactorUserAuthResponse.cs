using Shared.Enums.Auth;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Auth;

public record TwoFactorUserAuthResponse(
    Guid RequestId,
    UserDTO? UserDTO,
    AuthResult AuthResult,
    string? ServerMessage = null,
    string? SessionToken = null) : AuthResponse(RequestId);
