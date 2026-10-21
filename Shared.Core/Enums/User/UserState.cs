namespace Shared.Enums.User;

public enum UserState
{
    Unknown = 0,
    Banned = 1,
    Suspended = 2,
    Normal = 3,
    System
}

/// <summary>
/// What a user tells others about being there. The user picks <see cref="Online"/>, <see cref="Away"/>,
/// <see cref="DoNotDisturb"/> or <see cref="Invisible"/>; the others see <see cref="Offline"/> while the user is
/// not connected or is invisible.
/// </summary>
public enum UserStatus
{
    Offline = 0,
    Online = 1,
    Away = 2,
    DoNotDisturb = 3,
    Invisible = 4
}
