namespace Shared.Enums;

public enum HybridSessionJoinResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    SessionFull = 2,
    TimedOut = 3,
    InsufficientPermissions = 4,
    SFUError = 5,
    SFUNotActive = 6,
    UnknownSessionType = 7,
    Rejected = 8
}

public enum HybridSessionConnectTransportResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    TransportNotExists = 2,
    InvalidDtlsParameters = 3,
    SFUError = 4,
    SFUNotActive = 5,
    Rejected = 6
}

public enum HybridSessionLeaveResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    TimedOut = 2,
    InsufficientPermissions = 3,
    SFUError = 4,
    UnknownSessionType = 5,
    NotMemberOfSession = 6
}

public enum PeerListRequestResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    SessionIsNotHybrid = 2,
    Rejected = 3
}

public enum StartVoiceShareResult
{
    InternalError = -1,
    NoError = 0,
    UnsupportedMedia = 1,
    InsufficientPermissions = 2,
    SFUError = 3,
    SessionNotExists = 4,
    PeerNotExists = 5,
    SFUNotActive = 6,
    TimedOut = 7,
    Rejected = 8
}

public enum StopVoiceShareResult
{
    InternalError = -1,
    NoError = 0,
    VoiceShareNotActive = 1,
    InsufficientPermissions = 2,
    PeerNotExists = 3,
    SFUError = 4,
    SFUNotActive = 5,
    TimedOut = 6,
    Rejected = 7
}

public enum StartVideoShareResult
{
    InternalError = -1,
    NoError = 0,
    UnsupportedMedia = 1,
    InsufficientPermissions = 2,
    SFUError = 3,
    SessionNotExists = 4,
    PeerNotExists = 5,
    SFUNotActive = 6,
    TimedOut = 7,
    Rejected = 8
}

public enum StopVideoShareResult
{
    InternalError = -1,
    NoError = 0,
    VideoShareNotActive = 1,
    InsufficientPermissions = 2,
    PeerNotExists = 3,
    TimedOut = 4,
    Rejected = 5
}

public enum StartScreenShareResult
{
    InternalError = -1,
    NoError = 0,
    UnsupportedMedia = 1,
    InsufficientPermissions = 2,
    SFUError = 3,
    SessionNotExists = 4,
    PeerNotExists = 5,
    SFUNotActive = 6,
    TimedOut = 7,
    Rejected = 8
}

public enum StopScreenShareResult
{
    InternalError = -1,
    NoError = 0,
    ScreenShareNotActive = 1,
    InsufficientPermissions = 2,
    PeerNotExists = 3,
    SFUError = 4,
    SFUNotActive = 5,
    TimedOut = 6,
    Rejected = 7
}

public enum ResumeInboundsResult
{
    InternalError = -1,
    NoError = 0,
    SessionNotExists = 1,
    PeerNotExists = 2,
    SFUError = 3,
    SFUNotActive = 4,
    TimedOut = 5,
    Rejected = 6
}

public enum SetScreenShareWatchResult
{
    InternalError = -1,
    NoError = 0,
    ScreenShareNotActive = 1,
    InsufficientPermissions = 2,
    SFUError = 3,
    SessionNotExists = 4,
    PeerNotExists = 5,
    SFUNotActive = 6,
    TimedOut = 7,
    Rejected = 8
}

public enum UpdateVideoShareResult
{
    InternalError = -1,
    NoError = 0,
    UnsupportedMedia = 1,
    InsufficientPermissions = 2
}

public enum UpdateScreenShareResult
{
    InternalError = -1,
    NoError = 0,
    UnsupportedMedia = 1,
    InsufficientPermissions = 2,
    PeerNotExists = 3,
    SFUError = 4
}

public enum SetMicrophoneMuteResult
{
    InternalError = -1,
    NoError = 0,
    InsufficientPermissions = 1,
    SessionNotExists = 2,
    PeerNotExists = 3,
    VoiceNotStarted = 4,
    SFUError = 5,
    SFUNotActive = 6,
    TimedOut = 7,
    Rejected = 8
}

public enum SetSoundMuteResult
{
    InternalError = -1,
    NoError = 0,
    InsufficientPermissions = 1,
    SessionNotExists = 2,
    PeerNotExists = 3,
    TimedOut = 4,
    Rejected = 5
}

public enum MuteMicrophoneResult
{
    InternalError = -1,
    NoError = 0,
    InsufficientPermissions = 1
}

public enum UnmuteMicrophoneResult
{
    InternalError = -1,
    NoError = 0,
    InsufficientPermissions = 1
}

public enum MuteSoundResult
{
    InternalError = -1,
    NoError = 0,
    InsufficientPermissions = 1
}

public enum UnmuteSoundResult
{
    InternalError = -1,
    NoError = 0,
    InsufficientPermissions = 1
}

public enum SetAfkResult
{
    InternalError = -1,
    NoError = 0,
    InsufficientPermissions = 1,
    AfkModeDisabled = 2
}

public enum ResetAfkResult
{
    InternalError = -1,
    NoError = 0,
    InsufficientPermissions = 1,
    AfkModeDisabled = 2
}

public enum PokeResult
{
    InternalError = -1,
    NoError = 0,
    InsufficientPermissions = 1,
    PokeDisabled = 2
}
