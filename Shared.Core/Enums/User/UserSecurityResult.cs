namespace Shared.Enums.User;

/// <summary>The outcome of a request about the sender's password, one-time codes or passkeys.</summary>
public enum UserSecurityResult
{
    InternalError = -1,
    NoError = 0,

    /// <summary>The proof is missing or does not hold: the current password and/or one-time code.</summary>
    ProofRequired = 1,

    /// <summary>The value cannot be used: a password of the wrong length, codes enabled already, no secret offered...</summary>
    InvalidValue = 2,

    /// <summary>The code to turn codes on, or the new passkey, does not hold.</summary>
    Refused = 3,

    /// <summary>Passkeys are not set up on this server.</summary>
    PasskeysUnavailable = 4,

    PasskeyNotFound = 5
}
