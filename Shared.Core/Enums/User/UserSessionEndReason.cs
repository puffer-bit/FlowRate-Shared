namespace Shared.Enums.User;

/// <summary>Why the server ended the session of a connection.</summary>
public enum UserSessionEndReason
{
    /// <summary>The user ended it from another device, or the control panel did.</summary>
    Revoked = 0,

    /// <summary>The user got a new identity; the old one does not log in any more.</summary>
    IdentityReset = 1
}
