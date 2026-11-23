using System.Globalization;

namespace Shared.Protocol;

/// <summary>
/// The version of the wire protocol. The client sends it in a header of the WebSocket upgrade request,
/// before any message, so the check does not depend on the message format it guards.
/// </summary>
public static class ProtocolVersion
{
    /// <summary>
    /// Raised by every change old clients or servers cannot understand: a removed or renamed field,
    /// a changed type or enum number. A new request does not raise it: older servers answer it with
    /// ErrorCode.UnsupportedRequest. Shared.Tests fails on any
    /// change of the message schema until it is looked at.
    /// </summary>
    // 2: a message carries files (NewChatMessageModel.Files is a list of NewChatFileModel);
    //    sessions carry permission overrides; ChangeSessionPermissionsRequest; STUN/TURN fields removed;
    //    coordinator settings carry upload and rate limits; HybridSessionSetAfkRequest, HybridSessionPokeRequest
    //    and the PokeMembers permission; the User* security requests (password, one-time codes and
    //    passkeys of the sender); Outbound.TransportCcExtensionId; the watching screen share notifications
    //    and PeerDTO.WatchedScreenShares
    // 3: device sessions: UserAuthRequest.SessionToken and Device, SessionToken in the auth responses;
    //    UserSessionsRequest, UserSessionRevokeRequest, UserIdentityResetRequest and
    //    CoordinatorUserSessionEndedNotification; a failed request is answered with ErrorResponse and one
    //    ErrorCode instead of a Result in every response (RequestRejectedResponse and the *Result enums
    //    removed, AuthResult keeps Success and SecondStageRequired);
    //    the paired peer notifications (mute/unmute, afk/not afk, pause/resume, start/stop watching,
    //    SFU node connected/disconnected) are one *Changed notification each, with a flag;
    //    WebRtcConnectRequest, UserAuthRequest.IsFIDO2AuthRequested, HybridSessionDTO.IsHostConnected and
    //    the unused media fields of HybridSessionCreateData removed; acronyms upper case (SFU, DTO, DTLS,
    //    FIDO2, MOTD); fields named for what they hold (User, Coordinator, Sessions, Users, TargetUserId,
    //    SharerUserId, TransportData, Candidates, Message) and Send/Edit/DeleteMessage*,
    //    ChangeHybridSessionSettingsResponse; UserAuthResponse.SecondStages instead of ServerMessage, the
    //    second-stage responses carry the user and the token only;
    //    chat: ChatTypingRequest and ChatSessionUserTypingNotification.IsTyping, ChatViewersRequest and
    //    ChatViewersChangedNotification, SetMessageReactionRequest, SetMessagePinnedRequest,
    //    ChatPinnedMessagesRequest, ChatHistoryWindowRequest, ChatTextContentDTO.CommandResult,
    //    ChatMessageDTO.SystemContent; read receipts: ChatReadStateDTO.PeerReadMessageId,
    //    ChatPeerReadNotification, ChatMessageReadersRequest, ChatReactionDTO.ReactedAt; personal chats: ChatSessionDTO.Kind, MemberIds and LastMessage,
    //    ChangeGroupRequest; ChatCallRequest and ChatCallChangedNotification; global bans:
    //    CoordinatorBansRequest, CoordinatorRevokeBanRequest and the ban fields of CoordinatorSetUserStateRequest
    //    (BanInSessionRequest and HybridSessionPeerBannedNotification removed); AV1 in hybrid media settings.
    //    CoordinatorSettingsResponse.DatabaseProvider (read-only, optional); reactions accept Unicode emoji
    //    beyond the default quick choices, without changing their message format.
    //    Moving peers: HybridSessionMovePeerRequest and the MoveMembers permission; HybridSessionPeerMovedOutNotification
    //    carries TargetSessionId, HybridSessionPeerMovedInNotification the Peer and SourceSessionId.
    //    Release preparation: ErrorCode.UnsupportedRequest for requests the server does not know;
    //    LeaveGroupRequest instead of ChangeGroupRequest.Leave; CallNotFound, CallBusy and
    //    CallAnsweredElsewhere for direct calls (TargetIsHost removed); empty GUIDs refused.
    //    Bans: CoordinatorBanRequest instead of the ban fields of CoordinatorSetUserStateRequest, which only
    //    freezes and reinstates; BanNotFound; the BannedIps settings field removed (IP bans are bans).
    //    SenderId is no longer written: the server stamps it on what it receives.
    //    Message discriminators are fixed strings instead of nameof (same values), so a class rename keeps them.
    //    Times are Unix milliseconds (DateTimeOffset in C#) and durations milliseconds instead of ISO strings;
    //    moments are named *At: SentAt, ScheduledAt, ExpiresAt, EditedAt, CreatedAt, FirstLaunchedAt,
    //    LaunchedAt, FirstConnectedAt, LastConnectedAt, LastUsedAt, LastPingAt (no Utc suffixes).
    //    Sessions have no host: HybridSessionDTO.HostId removed, IsHostMustApprove -> IsApprovalRequired;
    //    ChatSessionDTO.AdminUserId and ChangeGroupRequest.OwnerId -> OwnerUserId.
    //    Attachments are one list of ChatFileDTO with derived ChatImageFileDTO/ChatVideoFileDTO (unknown kinds read as
    //    plain files), NewChatFileModel/NewChatImageModel likewise; ChatMessageDTO.Images/Video, ChatContentType and
    //    the legacy video content removed. Removed as unused or duplicated: UsersIds, IsReposted, IsDelivered,
    //    IsDelayed, IsEdited, IsDisposable (the times say it), ChatSessionCreateData.IsEncrypted,
    //    PeerDTO/HybridSessionDTO.IsAudioRequested, PeerDTO.IsNegotiated, ChatSessionHistoryRequest, JoinSessionRequest,
    //    HybridSessionLeaveRequest and SessionType of Leave/Kick/Approve/Reject.
    //    HybridSessionPeerMediaChangedNotification instead of the Started/Stopped share notifications; the unused
    //    Updated/PauseChanged/ScreenShareSoundMuteChanged ones removed.
    //    ServerRestartNotification and ServerRestartCancelledNotification instead of HeartbeatServerClosing/Restarting.
    //    Calls in direct chats and groups: ChatCallRequest/Response/ChangedNotification with per-member
    //    ChatCallParticipantDTO instead of DirectCall*; actions Start, Join, Decline, Leave, Ring; the call lives
    //    while anyone is in it; video and screen sharing allowed in calls.
    //    Pre-releases that announce 3 with an earlier schema, or 4-6, are not supported and will be withdrawn;
    //    the next breaking change after the release of 3 is 4.
    //    ChangeChatSessionSettingsRequest/Response update server chat channel names and descriptions.
    //    DeviceInfoDTO.OsVersion and Architecture (optional); UserDTO.Device (UserDeviceDTO), null for older servers.
    //    Group pictures: SetGroupAvatarRequest/Response and ChatSessionDTO.AvatarUrl instead of the unused
    //    IconPath (also removed from ChatSessionCreateData).
    //    Logging in by device keys: LoginChallengeRequest, LoginRequest with LoginMethodDTO (Device, Register,
    //    Recovery, Username, LoginCode, Legacy), LoginProofRequest, LoginPasskeyRequest and LoginResponse instead of
    //    UserAuthRequest, TwoFactorUserAuthRequest, FIDO2UserAuthRequest and their responses; SecondStageAuthType is
    //    LoginFactor. DeviceLinkStartRequest, DeviceLinkInfoRequest, DeviceLinkApproveRequest and the
    //    DeviceLinkCompleted/Declined notifications; AccountDevicesRequest, AccountDeviceRevokeRequest,
    //    LogoutRequest, AccountDeviceDTO and AccountDeviceEnded/AddedNotification instead of UserSessionsRequest,
    //    UserSessionRevokeRequest, UserSessionDTO and CoordinatorUserSessionEndedNotification;
    //    UserRecoveryKeyResetRequest instead of UserIdentityResetRequest; ErrorCode.DeviceNotFound,
    //    LoginFactorsMissing and DeviceLinkNotFound.
    public const int Current = 3;

    /// <summary>The oldest client version the server still serves.</summary>
    public const int MinSupported = 3;

    /// <summary>
    /// Request header with the client version. The server answers a version it does not serve with
    /// 426 Upgrade Required and both its headers.
    /// </summary>
    public const string Header = "FlowRate-Protocol";

    /// <summary>Response header with <see cref="MinSupported"/> of the server.</summary>
    public const string MinHeader = "FlowRate-Protocol-Min";

    public static string Format(int version) => version.ToString(CultureInfo.InvariantCulture);

    public static bool TryParse(string? value, out int version) =>
        int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out version) && version > 0;

    /// <summary>Whether the server serves a client that sent this header value; no header is never served.</summary>
    public static bool IsSupported(string? clientHeader) =>
        TryParse(clientHeader, out var version) && version is >= MinSupported and <= Current;
}
