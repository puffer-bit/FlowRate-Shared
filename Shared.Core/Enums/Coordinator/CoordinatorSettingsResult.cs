namespace Shared.Enums.Coordinator;

public enum CoordinatorSettingsResult
{
    InternalError = -1,
    NoError = 0,
    /// <summary>The settings were changed by someone else after the revision the change was based on.</summary>
    Conflict = 1,
    /// <summary>Some values were refused; the response tells which and why.</summary>
    InvalidSettings = 2,
    TimedOut = 3,
    Rejected = 4
}
