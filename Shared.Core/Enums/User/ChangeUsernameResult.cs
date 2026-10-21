namespace Shared.Enums.User;

public enum ChangeUsernameResult
{
    InternalError = -1,
    NoError = 0,
    UsernameAlreadyUsed = 1,
    UserNotFound = 2,
    InvalidUsername = 3,
    TimedOut = 4,
    Rejected = 5
}
