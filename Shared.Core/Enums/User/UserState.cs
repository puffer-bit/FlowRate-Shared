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
/// not connected or is invisible, and <see cref="Idle"/> or <see cref="Sleeping"/> instead of <see cref="Online"/>
/// while every device of the user reports no input for a while (<see cref="IdleLevel"/>).
/// </summary>
public enum UserStatus
{
    Offline = 0,
    Online = 1,
    Away = 2,
    DoNotDisturb = 3,
    Invisible = 4,

    /// <summary>Online, but no input on any device for some minutes. Shown only, never picked.</summary>
    Idle = 5,

    /// <summary>Online, but no input on any device for hours. Shown only, never picked.</summary>
    Sleeping = 6
}

/// <summary>How long a device of the user has gone without input, as its client reports it.</summary>
public enum IdleLevel
{
    Active = 0,
    Idle = 1,
    Sleeping = 2
}
