namespace Shared.Enums;

public enum CreateSessionResult
{
    InternalError = -1,
    NoError = 0,
    NameAlreadyUsed = 1,
    WrongCapacity = 2,
    UnexpectedParameters = 3,
    TimedOut = 4,
    Rejected = 5
}

public enum DeleteSessionResult
{
    InternalError = -1,
    NoError = 0,
    SessionContainsUsers = 1,
    SessionNotExists = 2,
    TimedOut = 3,
    Rejected = 4
}

public enum JoinSessionResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    SessionFull = 2,
    TimedOut = 3,
    InsufficientPermissions = 4,
    SFUError = 5,
    UnknownSessionType = 6
}

public enum LeaveSessionResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    TimedOut = 2,
    PeerNotExists = 3,
    UnknownSessionType = 4,
    SFUNotActive = 5,
    Rejected = 6,
    CannotKickSelf = 7
}

public enum LeaveFromSessionResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    TimedOut = 2,
    PeerNotExists = 3
}

public enum BanFromSessionResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    HostCannotBeBanned = 2,
    InsufficientPermissions = 3,
    TimedOut = 4,
    UnknownSessionType = 5
}

public enum ApproveUserSessionResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    TimedOut = 2,
    PeerNotExists = 3,
    AlreadyRejected = 4,
    SFUError = 5,
    UnknownSessionType = 6,
    SFUNotActive = 7
}

public enum RejectUserSessionResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    TimedOut = 2,
    PeerNotExists = 3,
    AlreadyApproved = 4,
    UnknownSessionType = 5,
    SFUNotActive = 6
}

public enum SessionRequestResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    TimedOut = 2,
    WrongResponse = 3,
    Rejected = 4
}

public enum SessionLayoutResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    CategoryNotExists = 2,
    TooManyCategories = 3,
    TimedOut = 4,
    Rejected = 5
}

public enum ChangeSessionSettingsResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    NameAlreadyUsed = 2,
    WrongCapacity = 3,
    TimedOut = 4,
    Rejected = 5
}

public enum ChangeSessionPermissionsResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    RoleNotExists = 2,

    /// <summary>A role not below one's own highest role, or a permission one does not have.</summary>
    NotAllowed = 3,
    TimedOut = 4,
    Rejected = 5
}
