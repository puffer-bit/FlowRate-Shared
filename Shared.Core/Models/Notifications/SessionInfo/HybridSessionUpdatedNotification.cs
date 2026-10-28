using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Media;

namespace Shared.Models.Notifications.SessionInfo;

public abstract record HybridSessionUpdatedNotification : Notification;

public record HybridSessionStateChangedNotification(int SessionId, HybridSessionNodeState State)
    : HybridSessionUpdatedNotification;

public record HybridSessionSFUNodeChangedNotification(int SessionId)
    : HybridSessionUpdatedNotification;

public record HybridSessionSFUNodeDisconnectedNotification(int SessionId)
    : HybridSessionUpdatedNotification;

public record HybridSessionSFUNodeConnectedNotification(int SessionId)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerStateChangedNotification(int SessionId, int UserId, HybridSessionPeerState State)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerJoinedNotification(int SessionId, PeerDTO Peer)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerLeftNotification(int SessionId, int UserId)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerKickedNotification(int SessionId, int UserId, int? InitiatorUserId = null, string? Reason = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerIdleKickedNotification(int SessionId, int UserId)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerBannedNotification(int SessionId, int UserId, int? InitiatorUserId = null, string? Reason = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerApprovedNotification(int SessionId, int UserId, int InitiatorUserId)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerRejectedNotification(int SessionId, int UserId, int? InitiatorUserId = null, string? Reason = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerMovedOutNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerMovedInNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;


public record HybridSessionPeerStartedScreenShareNotification(int SessionId, int UserId, Inbound? Inbound = null,
    bool HasAudio = false)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerUpdatedScreenShareNotification(int SessionId, int UserId, Inbound Inbound)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerStoppedScreenShareNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerMutedScreenShareSoundNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerPausedScreenShareNotification(int SessionId, int UserId, int? InitiatorUserId = null, string? Reason = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerUnmutedScreenShareSoundNotification(int SessionId, int UserId)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerResumedScreenShareNotification(int SessionId, int UserId)
    : HybridSessionUpdatedNotification;

/// <summary>The peer <paramref name="UserId"/> started watching the screen share of <paramref name="SharerUserId"/>.</summary>
public record HybridSessionPeerStartedWatchingScreenShareNotification(int SessionId, int UserId, int SharerUserId)
    : HybridSessionUpdatedNotification;

/// <summary>
/// The peer <paramref name="UserId"/> no longer watches the screen share of <paramref name="SharerUserId"/>: it stopped
/// watching, the share stopped or the sharer lost its media. Not sent for a peer that left the session.
/// </summary>
public record HybridSessionPeerStoppedWatchingScreenShareNotification(int SessionId, int UserId, int SharerUserId)
    : HybridSessionUpdatedNotification;


public record HybridSessionPeerStartedVideoShareNotification(int SessionId, int UserId, Inbound Inbound)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerUpdatedVideoShareNotification(int SessionId, int UserId, Inbound Inbound)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerStoppedVideoShareNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerPausedVideoShareNotification(int SessionId, int UserId, int? InitiatorUserId = null, string? Reason = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerResumedVideoShareNotification(int SessionId, int UserId, int? InitiatorUserId = null, string? Reason = null)
    : HybridSessionUpdatedNotification;


public record HybridSessionPeerStartedVoiceShareNotification(int SessionId, int UserId, Inbound Inbound)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerUpdatedVoiceShareNotification(int SessionId, int UserId, Inbound Inbound)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerStoppedVoiceShareNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerMuteMicrophoneNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerUnmuteMicrophoneNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerMuteSoundNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerUnmuteSoundNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;


public record HybridSessionPeerAfkNotification(int SessionId, int UserId, string? Reason = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerNotAfkNotification(int SessionId, int UserId)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerPokedNotification(int SessionId, int UserId, int InitiatorUserId, string? Message = null)
    : HybridSessionUpdatedNotification;
