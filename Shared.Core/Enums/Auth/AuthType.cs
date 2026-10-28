using Shared.Enums;

namespace Shared.Enums.Auth;

/// <summary>
/// How a login that did not fail goes on; a failed one is answered with an
/// <see cref="Models.Responses.ErrorResponse"/> (<see cref="ErrorCode.AuthFailed"/>, <see cref="ErrorCode.Banned"/>...).
/// </summary>
public enum AuthResult
{
    /// <summary>The sender is logged in.</summary>
    Success = 0,

    /// <summary>The login needs a second stage; the response tells which ones the user has.</summary>
    SecondStageRequired = 1
}

public enum SecondStageAuthType
{
    FIDO2 = 0,
    TwoFactor = 1,
    Password = 2
}
