namespace Shared.Enums.User;

/// <summary>How banning, freezing, reinstating or deleting a user went.</summary>
public enum ModerateUserResult
{
    InternalError = -1,
    NoError = 0,
    UserNotFound = 1,

    /// <summary>Users cannot ban, freeze or delete themselves, nor the system user.</summary>
    CannotTargetSelf = 2,

    /// <summary>A user can only be made normal, frozen or banned.</summary>
    InvalidState = 3,
    TimedOut = 4,
    Rejected = 5
}
