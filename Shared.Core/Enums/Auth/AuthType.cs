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

    /// <summary>The login needs a second stage; the response tells which factors the user has.</summary>
    SecondStageRequired = 1
}

/// <summary>What a user can prove in the second stage of a login by username.</summary>
public enum LoginFactor
{
    Passkey = 0,
    TwoFactor = 1,
    Password = 2
}

/// <summary>How a device was added to an account.</summary>
public enum DeviceLoginMethod
{
    /// <summary>The account was registered from it.</summary>
    Registration = 0,

    /// <summary>With the recovery phrase.</summary>
    Recovery = 1,

    /// <summary>With the username and the password, one-time code or passkey.</summary>
    Factors = 2,

    /// <summary>With a one-time login code from the control panel.</summary>
    LoginCode = 3,

    /// <summary>Approved from another device of the account.</summary>
    DeviceLink = 4,

    /// <summary>Moved over from the identity of protocol 3 before devices.</summary>
    Legacy = 5
}

/// <summary>Why the server ended a device while it was connected.</summary>
public enum DeviceEndReason
{
    /// <summary>Another device of the user, or the control panel, removed it.</summary>
    Revoked = 0,

    /// <summary>The device logged out.</summary>
    LoggedOut = 1
}
