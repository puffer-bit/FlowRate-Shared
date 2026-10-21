namespace Shared.Enums;

public enum RequestRejectReason
{
    RateLimited = 1,
    InvalidRequest = 2,

    /// <summary>The sender's roles do not allow the request.</summary>
    InsufficientPermissions = 3,

    /// <summary>The sender is frozen: they may read, but not write, join voice or change anything.</summary>
    UserFrozen = 4
}
