namespace Shared.Enums;

public enum SessionsStateChangedType
{
    HybridSessionCreated = 7,
    HybridSessionDeleted = 8,
    HybridSessionReconfigured = 9,
    ChatSessionCreated = 10,
    ChatSessionDeleted = 11,
    ChatSessionReconfigured = 12,
}

public enum HybridSessionStateChangedType
{
    PeerConnected = 1,
    PeerDisconnected,
    PeerKicked,
    PeerKickedIdle,
    PeerBanned,
    PeerJoined,
    PeerApproved,
    PeerRejected,
    PeerLeft,
    PeerStartedScreenShare,
    PeerUpdatedScreenShare,
    PeerStoppedScreenShare,
    PeerMutedScreenShareSound,
    PeerPausedScreenShare,
    PeerUnmutedScreenShareSound,
    PeerResumedScreenShare,
    PeerStartedVideoShare,
    PeerUpdatedVideoShare,
    PeerStoppedVideoShare,
    PeerPausedVideoShare,
    PeerResumedVideoShare,
    PeerStartedVoiceShare,
    PeerUpdatedVoiceShare,
    PeerStoppedVoiceShare,
    PeerPausedVoiceShare,
    PeerMuteMicrophone,
    PeerUnmuteMicrophone,
    PeerMuteSound,
    PeerUnmuteSound,
    PeerAfk,
    PeerNotAfk,
    PeerPoked,
    PeerMovedOut,
    PeerMovedIn,
    PeerStateChanged,
    SFUNodeChanged,
    SFUNodeDisconnected,
    SFUNodeConnected,
    StateChanged
}

public enum ChatSessionStateChangedType
{
    UserTyping = 1,
    MessageAdded = 2,
    MessageEdited,
    MessageDeleted
}
