using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Media;

namespace Shared.Models.Notifications.SessionInfo;

public abstract record HybridSessionUpdatedNotification : Notification;

public record HybridSessionStateChangedNotification(int SessionId, HybridSessionNodeState State)
    : HybridSessionUpdatedNotification;

public record HybridSessionSFUNodeChangedNotification(int SessionId)
    : HybridSessionUpdatedNotification;

public record HybridSessionSFUNodeConnectionChangedNotification(int SessionId, bool IsConnected)
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

public record HybridSessionPeerScreenShareSoundMuteChangedNotification(int SessionId, int UserId, bool IsMuted,
    int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerScreenSharePauseChangedNotification(int SessionId, int UserId, bool IsPaused,
    int? InitiatorUserId = null, string? Reason = null)
    : HybridSessionUpdatedNotification;

/// <summary>
/// The peer <paramref name="UserId"/> started or stopped watching the screen share of <paramref name="SharerUserId"/>.
/// It stops watching when it asks to, when the share stops or when the sharer loses its media; not sent for a peer
/// that left the session.
/// </summary>
public record HybridSessionPeerScreenShareWatchChangedNotification(int SessionId, int UserId, int SharerUserId,
    bool IsWatching)
    : HybridSessionUpdatedNotification;


public record HybridSessionPeerStartedVideoShareNotification(int SessionId, int UserId, Inbound Inbound)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerUpdatedVideoShareNotification(int SessionId, int UserId, Inbound Inbound)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerStoppedVideoShareNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerVideoSharePauseChangedNotification(int SessionId, int UserId, bool IsPaused,
    int? InitiatorUserId = null, string? Reason = null)
    : HybridSessionUpdatedNotification;


public record HybridSessionPeerStartedVoiceShareNotification(int SessionId, int UserId, Inbound Inbound)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerUpdatedVoiceShareNotification(int SessionId, int UserId, Inbound Inbound)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerStoppedVoiceShareNotification(int SessionId, int UserId, int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerMicrophoneMuteChangedNotification(int SessionId, int UserId, bool IsMuted,
    int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerSoundMuteChangedNotification(int SessionId, int UserId, bool IsMuted,
    int? InitiatorUserId = null)
    : HybridSessionUpdatedNotification;


/// <param name="Reason">Why the peer went away; only with <paramref name="IsAfk"/>.</param>
public record HybridSessionPeerAfkChangedNotification(int SessionId, int UserId, bool IsAfk, string? Reason = null)
    : HybridSessionUpdatedNotification;

public record HybridSessionPeerPokedNotification(int SessionId, int UserId, int InitiatorUserId, string? Message = null)
    : HybridSessionUpdatedNotification;
