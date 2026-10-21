namespace Shared.Enums.User;

public enum SetUserStatusResult
{
    InternalError = -1,
    NoError = 0,
    UserNotFound = 1,

    /// <summary>A user can pick online, away, do not disturb or invisible; offline is not a choice.</summary>
    InvalidStatus = 2,
    TimedOut = 3,
    Rejected = 4
}
