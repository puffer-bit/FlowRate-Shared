using System.Text.Json.Serialization;
using Shared.Models.Notifications.CoordinatorInfo;
using Shared.Models.Notifications.Heartbeat;
using Shared.Models.Notifications.SessionInfo;
using Shared.Models.Requests.Auth;
using Shared.Models.Requests.Coordinator;
using Shared.Models.Requests.Coordinator.Security;
using Shared.Models.Requests.Heartbeat;
using Shared.Models.Requests.SessionActions.ChatSession;
using Shared.Models.Requests.SessionActions.Generic;
using Shared.Models.Requests.SessionActions.HybridSession;
using Shared.Models.Responses;
using Shared.Models.Responses.Auth;
using Shared.Models.Responses.Coordinator;
using Shared.Models.Responses.Coordinator.Security;
using Shared.Models.Responses.Heartbeat;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Models.Responses.SessionActions.HybridSession;

namespace Shared.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(ChangeChatSessionSettingsRequest), typeDiscriminator: "ChangeChatSessionSettingsRequest")]
[JsonDerivedType(typeof(ChangeChatSessionSettingsResponse), typeDiscriminator: "ChangeChatSessionSettingsResponse")]
[JsonDerivedType(typeof(ChatCallRequest), typeDiscriminator: "ChatCallRequest")]
[JsonDerivedType(typeof(ChatCallResponse), typeDiscriminator: "ChatCallResponse")]
[JsonDerivedType(typeof(ChatCallChangedNotification), typeDiscriminator: "ChatCallChangedNotification")]

[JsonDerivedType(typeof(ChatViewersRequest), typeDiscriminator: "ChatViewersRequest")]
[JsonDerivedType(typeof(ChatViewersResponse), typeDiscriminator: "ChatViewersResponse")]
[JsonDerivedType(typeof(ChatViewersChangedNotification), typeDiscriminator: "ChatViewersChangedNotification")]

[JsonDerivedType(typeof(SetMessagePinnedRequest), typeDiscriminator: "SetMessagePinnedRequest")]

[JsonDerivedType(typeof(SetMessagePinnedResponse), typeDiscriminator: "SetMessagePinnedResponse")]

[JsonDerivedType(typeof(ChatPinnedMessagesRequest), typeDiscriminator: "ChatPinnedMessagesRequest")]

[JsonDerivedType(typeof(ChatPinnedMessagesResponse), typeDiscriminator: "ChatPinnedMessagesResponse")]

[JsonDerivedType(typeof(ChatHistoryWindowRequest), typeDiscriminator: "ChatHistoryWindowRequest")]

[JsonDerivedType(typeof(ChatHistoryWindowResponse), typeDiscriminator: "ChatHistoryWindowResponse")]

[JsonDerivedType(typeof(ChangeGroupRequest), typeDiscriminator: "ChangeGroupRequest")]
[JsonDerivedType(typeof(ChangeGroupResponse), typeDiscriminator: "ChangeGroupResponse")]
[JsonDerivedType(typeof(SetGroupAvatarRequest), typeDiscriminator: "SetGroupAvatarRequest")]
[JsonDerivedType(typeof(SetGroupAvatarResponse), typeDiscriminator: "SetGroupAvatarResponse")]
[JsonDerivedType(typeof(LeaveGroupRequest), typeDiscriminator: "LeaveGroupRequest")]
[JsonDerivedType(typeof(LeaveGroupResponse), typeDiscriminator: "LeaveGroupResponse")]

// Notifications

[JsonDerivedType(typeof(CoordinatorUserListUpdatedNotification), typeDiscriminator: "CoordinatorUserListUpdatedNotification")]
[JsonDerivedType(typeof(CoordinatorUserConnectedNotification), typeDiscriminator: "CoordinatorUserConnectedNotification")]
[JsonDerivedType(typeof(CoordinatorUserUpdatedNotification), typeDiscriminator: "CoordinatorUserUpdatedNotification")]
[JsonDerivedType(typeof(CoordinatorUserDisconnectedNotification), typeDiscriminator: "CoordinatorUserDisconnectedNotification")]
[JsonDerivedType(typeof(CoordinatorUserKickedNotification), typeDiscriminator: "CoordinatorUserKickedNotification")]
[JsonDerivedType(typeof(CoordinatorUserBannedNotification), typeDiscriminator: "CoordinatorUserBannedNotification")]
[JsonDerivedType(typeof(CoordinatorUserSessionEndedNotification), typeDiscriminator: "CoordinatorUserSessionEndedNotification")]
[JsonDerivedType(typeof(CoordinatorUserDeletedNotification), typeDiscriminator: "CoordinatorUserDeletedNotification")]
[JsonDerivedType(typeof(CoordinatorOnlineUsersChangedNotification), typeDiscriminator: "CoordinatorOnlineUsersChangedNotification")]
[JsonDerivedType(typeof(CoordinatorReconfiguredNotification), typeDiscriminator: "CoordinatorReconfiguredNotification")]
[JsonDerivedType(typeof(CoordinatorRolesChangedNotification), typeDiscriminator: "CoordinatorRolesChangedNotification")]
[JsonDerivedType(typeof(CoordinatorPermissionsChangedNotification), typeDiscriminator: "CoordinatorPermissionsChangedNotification")]

[JsonDerivedType(typeof(ServerRestartNotification), typeDiscriminator: "ServerRestartNotification")]
[JsonDerivedType(typeof(ServerRestartCancelledNotification), typeDiscriminator: "ServerRestartCancelledNotification")]

[JsonDerivedType(typeof(ChatSessionMessageAddedNotification), typeDiscriminator: "ChatSessionMessageAddedNotification")]
[JsonDerivedType(typeof(ChatSessionMessageEditedNotification), typeDiscriminator: "ChatSessionMessageEditedNotification")]
[JsonDerivedType(typeof(ChatSessionMessageDeletedNotification), typeDiscriminator: "ChatSessionMessageDeletedNotification")]
[JsonDerivedType(typeof(ChatSessionUserTypingNotification), typeDiscriminator: "ChatSessionUserTypingNotification")]
[JsonDerivedType(typeof(ChatReadStateChangedNotification), typeDiscriminator: "ChatReadStateChangedNotification")]
[JsonDerivedType(typeof(ChatPeerReadNotification), typeDiscriminator: "ChatPeerReadNotification")]

[JsonDerivedType(typeof(HybridSessionCreatedNotification), typeDiscriminator: "HybridSessionCreatedNotification")]
[JsonDerivedType(typeof(HybridSessionReconfiguredNotification), typeDiscriminator: "HybridSessionReconfiguredNotification")]
[JsonDerivedType(typeof(HybridSessionDeletedNotification), typeDiscriminator: "HybridSessionDeletedNotification")]
[JsonDerivedType(typeof(ChatSessionCreatedNotification), typeDiscriminator: "ChatSessionCreatedNotification")]
[JsonDerivedType(typeof(ChatSessionReconfiguredNotification), typeDiscriminator: "ChatSessionReconfiguredNotification")]
[JsonDerivedType(typeof(ChatSessionDeletedNotification), typeDiscriminator: "ChatSessionDeletedNotification")]
[JsonDerivedType(typeof(SessionLayoutChangedNotification), typeDiscriminator: "SessionLayoutChangedNotification")]

[JsonDerivedType(typeof(HybridSessionPeerStateChangedNotification), typeDiscriminator: "HybridSessionPeerStateChangedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerApprovedNotification), typeDiscriminator: "HybridSessionPeerApprovedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerRejectedNotification), typeDiscriminator: "HybridSessionPeerRejectedNotification")]
[JsonDerivedType(typeof(HybridSessionStateChangedNotification), typeDiscriminator: "HybridSessionStateChangedNotification")]
[JsonDerivedType(typeof(HybridSessionSFUNodeChangedNotification), typeDiscriminator: "HybridSessionSFUNodeChangedNotification")]
[JsonDerivedType(typeof(HybridSessionSFUNodeConnectionChangedNotification), typeDiscriminator: "HybridSessionSFUNodeConnectionChangedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerJoinedNotification), typeDiscriminator: "HybridSessionPeerJoinedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerLeftNotification), typeDiscriminator: "HybridSessionPeerLeftNotification")]
[JsonDerivedType(typeof(HybridSessionPeerKickedNotification), typeDiscriminator: "HybridSessionPeerKickedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerIdleKickedNotification), typeDiscriminator: "HybridSessionPeerIdleKickedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerMovedOutNotification), typeDiscriminator: "HybridSessionPeerMovedOutNotification")]
[JsonDerivedType(typeof(HybridSessionPeerMovedInNotification), typeDiscriminator: "HybridSessionPeerMovedInNotification")]
[JsonDerivedType(typeof(HybridSessionPeerScreenShareWatchChangedNotification), typeDiscriminator: "HybridSessionPeerScreenShareWatchChangedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerMicrophoneMuteChangedNotification), typeDiscriminator: "HybridSessionPeerMicrophoneMuteChangedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerMediaChangedNotification), typeDiscriminator: "HybridSessionPeerMediaChangedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerSoundMuteChangedNotification), typeDiscriminator: "HybridSessionPeerSoundMuteChangedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerAfkChangedNotification), typeDiscriminator: "HybridSessionPeerAfkChangedNotification")]
[JsonDerivedType(typeof(HybridSessionPeerPokedNotification), typeDiscriminator: "HybridSessionPeerPokedNotification")]

// Requests

[JsonDerivedType(typeof(UserAuthRequest), typeDiscriminator: "UserAuthRequest")]
[JsonDerivedType(typeof(TwoFactorUserAuthRequest), typeDiscriminator: "TwoFactorUserAuthRequest")]
[JsonDerivedType(typeof(FIDO2UserAuthRequest), typeDiscriminator: "FIDO2UserAuthRequest")]

[JsonDerivedType(typeof(CoordinatorInfoRequest), typeDiscriminator: "CoordinatorInfoRequest")]
[JsonDerivedType(typeof(CoordinatorUserInfoRequest), typeDiscriminator: "CoordinatorUserInfoRequest")]
[JsonDerivedType(typeof(CoordinatorUserSearchRequest), typeDiscriminator: "CoordinatorUserSearchRequest")]
[JsonDerivedType(typeof(CoordinatorUserListRequest), typeDiscriminator: "CoordinatorUserListRequest")]
[JsonDerivedType(typeof(CoordinatorChangeUsernameRequest), typeDiscriminator: "CoordinatorChangeUsernameRequest")]
[JsonDerivedType(typeof(CoordinatorSetAvatarRequest), typeDiscriminator: "CoordinatorSetAvatarRequest")]
[JsonDerivedType(typeof(CoordinatorSetUserStatusRequest), typeDiscriminator: "CoordinatorSetUserStatusRequest")]
[JsonDerivedType(typeof(CoordinatorBansRequest), typeDiscriminator: "CoordinatorBansRequest")]
[JsonDerivedType(typeof(CoordinatorRevokeBanRequest), typeDiscriminator: "CoordinatorRevokeBanRequest")]
[JsonDerivedType(typeof(CoordinatorBanRequest), typeDiscriminator: "CoordinatorBanRequest")]
[JsonDerivedType(typeof(CoordinatorBanResponse), typeDiscriminator: "CoordinatorBanResponse")]
[JsonDerivedType(typeof(CoordinatorBansResponse), typeDiscriminator: "CoordinatorBansResponse")]
[JsonDerivedType(typeof(CoordinatorRevokeBanResponse), typeDiscriminator: "CoordinatorRevokeBanResponse")]
[JsonDerivedType(typeof(CoordinatorSetUserStateRequest), typeDiscriminator: "CoordinatorSetUserStateRequest")]
[JsonDerivedType(typeof(CoordinatorDeleteUserRequest), typeDiscriminator: "CoordinatorDeleteUserRequest")]
[JsonDerivedType(typeof(CoordinatorSettingsRequest), typeDiscriminator: "CoordinatorSettingsRequest")]
[JsonDerivedType(typeof(CoordinatorUpdateSettingsRequest), typeDiscriminator: "CoordinatorUpdateSettingsRequest")]
[JsonDerivedType(typeof(CoordinatorRolesRequest), typeDiscriminator: "CoordinatorRolesRequest")]
[JsonDerivedType(typeof(CoordinatorCreateRoleRequest), typeDiscriminator: "CoordinatorCreateRoleRequest")]
[JsonDerivedType(typeof(CoordinatorUpdateRoleRequest), typeDiscriminator: "CoordinatorUpdateRoleRequest")]
[JsonDerivedType(typeof(CoordinatorDeleteRoleRequest), typeDiscriminator: "CoordinatorDeleteRoleRequest")]
[JsonDerivedType(typeof(CoordinatorReorderRolesRequest), typeDiscriminator: "CoordinatorReorderRolesRequest")]
[JsonDerivedType(typeof(CoordinatorAssignRoleRequest), typeDiscriminator: "CoordinatorAssignRoleRequest")]
[JsonDerivedType(typeof(CoordinatorUnassignRoleRequest), typeDiscriminator: "CoordinatorUnassignRoleRequest")]
[JsonDerivedType(typeof(UserSecurityInfoRequest), typeDiscriminator: "UserSecurityInfoRequest")]
[JsonDerivedType(typeof(UserSetPasswordRequest), typeDiscriminator: "UserSetPasswordRequest")]
[JsonDerivedType(typeof(UserSessionsRequest), typeDiscriminator: "UserSessionsRequest")]
[JsonDerivedType(typeof(UserSessionRevokeRequest), typeDiscriminator: "UserSessionRevokeRequest")]
[JsonDerivedType(typeof(UserIdentityResetRequest), typeDiscriminator: "UserIdentityResetRequest")]
[JsonDerivedType(typeof(UserTwoFactorSetupRequest), typeDiscriminator: "UserTwoFactorSetupRequest")]
[JsonDerivedType(typeof(UserTwoFactorEnableRequest), typeDiscriminator: "UserTwoFactorEnableRequest")]
[JsonDerivedType(typeof(UserTwoFactorDisableRequest), typeDiscriminator: "UserTwoFactorDisableRequest")]
[JsonDerivedType(typeof(UserPasskeyOptionsRequest), typeDiscriminator: "UserPasskeyOptionsRequest")]
[JsonDerivedType(typeof(UserPasskeyRegisterRequest), typeDiscriminator: "UserPasskeyRegisterRequest")]
[JsonDerivedType(typeof(UserPasskeyDeleteRequest), typeDiscriminator: "UserPasskeyDeleteRequest")]
[JsonDerivedType(typeof(RegisterPushTokenRequest), typeDiscriminator: "RegisterPushTokenRequest")]
[JsonDerivedType(typeof(UnregisterPushTokenRequest), typeDiscriminator: "UnregisterPushTokenRequest")]
[JsonDerivedType(typeof(MediaTokenRequest), typeDiscriminator: "MediaTokenRequest")]

[JsonDerivedType(typeof(HeartbeatPingRequest), typeDiscriminator: "HeartbeatPingRequest")]

[JsonDerivedType(typeof(ChatSessionMessageRequest), typeDiscriminator: "ChatSessionMessageRequest")]
[JsonDerivedType(typeof(ChatSessionHistoryPageRequest), typeDiscriminator: "ChatSessionHistoryPageRequest")]
[JsonDerivedType(typeof(ChatSessionChangesRequest), typeDiscriminator: "ChatSessionChangesRequest")]
[JsonDerivedType(typeof(ChatReadStatesRequest), typeDiscriminator: "ChatReadStatesRequest")]
[JsonDerivedType(typeof(ChatTypingRequest), typeDiscriminator: "ChatTypingRequest")]
[JsonDerivedType(typeof(ChatMarkReadRequest), typeDiscriminator: "ChatMarkReadRequest")]
[JsonDerivedType(typeof(ChatMessageReadersRequest), typeDiscriminator: "ChatMessageReadersRequest")]
[JsonDerivedType(typeof(ChatLinkPreviewRequest), typeDiscriminator: "ChatLinkPreviewRequest")]
[JsonDerivedType(typeof(DeleteMessageRequest), typeDiscriminator: "DeleteMessageRequest")]
[JsonDerivedType(typeof(EditMessageRequest), typeDiscriminator: "EditMessageRequest")]
[JsonDerivedType(typeof(SetMessageReactionRequest), typeDiscriminator: "SetMessageReactionRequest")]
[JsonDerivedType(typeof(SendMessageRequest), typeDiscriminator: "SendMessageRequest")]

[JsonDerivedType(typeof(ApproveUserRequest), typeDiscriminator: "ApproveUserRequest")]
[JsonDerivedType(typeof(CreateSessionRequest), typeDiscriminator: "CreateSessionRequest")]
[JsonDerivedType(typeof(DeleteSessionRequest), typeDiscriminator: "DeleteSessionRequest")]
[JsonDerivedType(typeof(KickFromSessionRequest), typeDiscriminator: "KickFromSessionRequest")]
[JsonDerivedType(typeof(ChangeHybridSessionSettingsRequest), typeDiscriminator: "ChangeHybridSessionSettingsRequest")]
[JsonDerivedType(typeof(ChangeSessionPermissionsRequest), typeDiscriminator: "ChangeSessionPermissionsRequest")]
[JsonDerivedType(typeof(LeaveSessionRequest), typeDiscriminator: "LeaveSessionRequest")]
[JsonDerivedType(typeof(RejectUserRequest), typeDiscriminator: "RejectUserRequest")]
[JsonDerivedType(typeof(SessionInfoRequest), typeDiscriminator: "SessionInfoRequest")]
[JsonDerivedType(typeof(CreateSessionCategoryRequest), typeDiscriminator: "CreateSessionCategoryRequest")]
[JsonDerivedType(typeof(RenameSessionCategoryRequest), typeDiscriminator: "RenameSessionCategoryRequest")]
[JsonDerivedType(typeof(DeleteSessionCategoryRequest), typeDiscriminator: "DeleteSessionCategoryRequest")]
[JsonDerivedType(typeof(MoveSessionRequest), typeDiscriminator: "MoveSessionRequest")]
[JsonDerivedType(typeof(MoveSessionCategoryRequest), typeDiscriminator: "MoveSessionCategoryRequest")]

[JsonDerivedType(typeof(HybridSessionJoinRequest), typeDiscriminator: "HybridSessionJoinRequest")]
[JsonDerivedType(typeof(HybridSessionConnectTransportRequest), typeDiscriminator: "HybridSessionConnectTransportRequest")]
[JsonDerivedType(typeof(HybridSessionPeerListRequest), typeDiscriminator: "HybridSessionPeerListRequest")]
[JsonDerivedType(typeof(HybridSessionStartVideoRequest), typeDiscriminator: "HybridSessionStartVideoRequest")]
[JsonDerivedType(typeof(HybridSessionStopVideoRequest), typeDiscriminator: "HybridSessionStopVideoRequest")]
[JsonDerivedType(typeof(HybridSessionStartScreenShareRequest), typeDiscriminator: "HybridSessionStartScreenShareRequest")]
[JsonDerivedType(typeof(HybridSessionStopScreenShareRequest), typeDiscriminator: "HybridSessionStopScreenShareRequest")]
[JsonDerivedType(typeof(HybridSessionSetScreenShareWatchRequest), typeDiscriminator: "HybridSessionSetScreenShareWatchRequest")]
[JsonDerivedType(typeof(HybridSessionResumeInboundsRequest), typeDiscriminator: "HybridSessionResumeInboundsRequest")]
[JsonDerivedType(typeof(HybridSessionStartVoiceRequest), typeDiscriminator: "HybridSessionStartVoiceRequest")]
[JsonDerivedType(typeof(HybridSessionStopVoiceRequest), typeDiscriminator: "HybridSessionStopVoiceRequest")]
[JsonDerivedType(typeof(HybridSessionSetMicrophoneMuteRequest), typeDiscriminator: "HybridSessionSetMicrophoneMuteRequest")]
[JsonDerivedType(typeof(HybridSessionSetSoundMuteRequest), typeDiscriminator: "HybridSessionSetSoundMuteRequest")]
[JsonDerivedType(typeof(HybridSessionSetAfkRequest), typeDiscriminator: "HybridSessionSetAfkRequest")]
[JsonDerivedType(typeof(HybridSessionPokeRequest), typeDiscriminator: "HybridSessionPokeRequest")]
[JsonDerivedType(typeof(HybridSessionMovePeerRequest), typeDiscriminator: "HybridSessionMovePeerRequest")]


// Responses

[JsonDerivedType(typeof(UserAuthResponse), typeDiscriminator: "UserAuthResponse")]
[JsonDerivedType(typeof(TwoFactorUserAuthResponse), typeDiscriminator: "TwoFactorUserAuthResponse")]
[JsonDerivedType(typeof(FIDO2UserAuthResponse), typeDiscriminator: "FIDO2UserAuthResponse")]

[JsonDerivedType(typeof(CoordinatorInfoResponse), typeDiscriminator: "CoordinatorInfoResponse")]
[JsonDerivedType(typeof(CoordinatorUserInfoResponse), typeDiscriminator: "CoordinatorUserInfoResponse")]
[JsonDerivedType(typeof(CoordinatorUserSearchResponse), typeDiscriminator: "CoordinatorUserSearchResponse")]
[JsonDerivedType(typeof(CoordinatorUserListResponse), typeDiscriminator: "CoordinatorUserListResponse")]
[JsonDerivedType(typeof(CoordinatorChangeUsernameResponse), typeDiscriminator: "CoordinatorChangeUsernameResponse")]
[JsonDerivedType(typeof(CoordinatorSetAvatarResponse), typeDiscriminator: "CoordinatorSetAvatarResponse")]
[JsonDerivedType(typeof(CoordinatorSetUserStatusResponse), typeDiscriminator: "CoordinatorSetUserStatusResponse")]
[JsonDerivedType(typeof(CoordinatorSetUserStateResponse), typeDiscriminator: "CoordinatorSetUserStateResponse")]
[JsonDerivedType(typeof(CoordinatorDeleteUserResponse), typeDiscriminator: "CoordinatorDeleteUserResponse")]
[JsonDerivedType(typeof(CoordinatorSettingsResponse), typeDiscriminator: "CoordinatorSettingsResponse")]
[JsonDerivedType(typeof(CoordinatorUpdateSettingsResponse), typeDiscriminator: "CoordinatorUpdateSettingsResponse")]
[JsonDerivedType(typeof(CoordinatorRolesResponse), typeDiscriminator: "CoordinatorRolesResponse")]
[JsonDerivedType(typeof(CoordinatorRoleResponse), typeDiscriminator: "CoordinatorRoleResponse")]
[JsonDerivedType(typeof(CoordinatorUserRolesResponse), typeDiscriminator: "CoordinatorUserRolesResponse")]
[JsonDerivedType(typeof(UserSecurityResponse), typeDiscriminator: "UserSecurityResponse")]
[JsonDerivedType(typeof(UserSessionsResponse), typeDiscriminator: "UserSessionsResponse")]
[JsonDerivedType(typeof(UserIdentityResetResponse), typeDiscriminator: "UserIdentityResetResponse")]
[JsonDerivedType(typeof(UserTwoFactorSetupResponse), typeDiscriminator: "UserTwoFactorSetupResponse")]
[JsonDerivedType(typeof(UserPasskeyOptionsResponse), typeDiscriminator: "UserPasskeyOptionsResponse")]
[JsonDerivedType(typeof(RegisterPushTokenResponse), typeDiscriminator: "RegisterPushTokenResponse")]
[JsonDerivedType(typeof(UnregisterPushTokenResponse), typeDiscriminator: "UnregisterPushTokenResponse")]
[JsonDerivedType(typeof(MediaTokenResponse), typeDiscriminator: "MediaTokenResponse")]

[JsonDerivedType(typeof(HeartbeatPingResponse), typeDiscriminator: "HeartbeatPingResponse")]

[JsonDerivedType(typeof(ChatSessionMessageResponse), typeDiscriminator: "ChatSessionMessageResponse")]
[JsonDerivedType(typeof(ChatSessionHistoryPageResponse), typeDiscriminator: "ChatSessionHistoryPageResponse")]
[JsonDerivedType(typeof(ChatSessionChangesResponse), typeDiscriminator: "ChatSessionChangesResponse")]
[JsonDerivedType(typeof(ChatReadStatesResponse), typeDiscriminator: "ChatReadStatesResponse")]
[JsonDerivedType(typeof(ChatTypingResponse), typeDiscriminator: "ChatTypingResponse")]
[JsonDerivedType(typeof(ChatMarkReadResponse), typeDiscriminator: "ChatMarkReadResponse")]
[JsonDerivedType(typeof(ChatMessageReadersResponse), typeDiscriminator: "ChatMessageReadersResponse")]
[JsonDerivedType(typeof(ChatLinkPreviewResponse), typeDiscriminator: "ChatLinkPreviewResponse")]
[JsonDerivedType(typeof(DeleteMessageResponse), typeDiscriminator: "DeleteMessageResponse")]
[JsonDerivedType(typeof(EditMessageResponse), typeDiscriminator: "EditMessageResponse")]
[JsonDerivedType(typeof(SetMessageReactionResponse), typeDiscriminator: "SetMessageReactionResponse")]
[JsonDerivedType(typeof(SendMessageResponse), typeDiscriminator: "SendMessageResponse")]

[JsonDerivedType(typeof(ApproveUserResponse), typeDiscriminator: "ApproveUserResponse")]
[JsonDerivedType(typeof(CreateSessionResponse), typeDiscriminator: "CreateSessionResponse")]
[JsonDerivedType(typeof(DeleteSessionResponse), typeDiscriminator: "DeleteSessionResponse")]
[JsonDerivedType(typeof(KickFromSessionResponse), typeDiscriminator: "KickFromSessionResponse")]
[JsonDerivedType(typeof(ChangeHybridSessionSettingsResponse), typeDiscriminator: "ChangeHybridSessionSettingsResponse")]
[JsonDerivedType(typeof(ChangeSessionPermissionsResponse), typeDiscriminator: "ChangeSessionPermissionsResponse")]
[JsonDerivedType(typeof(LeaveSessionResponse), typeDiscriminator: "LeaveSessionResponse")]
[JsonDerivedType(typeof(RejectUserResponse), typeDiscriminator: "RejectUserResponse")]
[JsonDerivedType(typeof(SessionInfoResponse), typeDiscriminator: "SessionInfoResponse")]
[JsonDerivedType(typeof(SessionLayoutResponse), typeDiscriminator: "SessionLayoutResponse")]

[JsonDerivedType(typeof(HybridSessionJoinResponse), typeDiscriminator: "HybridSessionJoinResponse")]
[JsonDerivedType(typeof(HybridSessionConnectTransportResponse), typeDiscriminator: "HybridSessionConnectTransportResponse")]
[JsonDerivedType(typeof(HybridSessionPeerListResponse), typeDiscriminator: "HybridSessionPeerListResponse")]
[JsonDerivedType(typeof(HybridSessionStartVideoResponse), typeDiscriminator: "HybridSessionStartVideoResponse")]
[JsonDerivedType(typeof(HybridSessionStopVideoResponse), typeDiscriminator: "HybridSessionStopVideoResponse")]
[JsonDerivedType(typeof(HybridSessionStartScreenShareResponse), typeDiscriminator: "HybridSessionStartScreenShareResponse")]
[JsonDerivedType(typeof(HybridSessionStopScreenShareResponse), typeDiscriminator: "HybridSessionStopScreenShareResponse")]
[JsonDerivedType(typeof(HybridSessionSetScreenShareWatchResponse), typeDiscriminator: "HybridSessionSetScreenShareWatchResponse")]
[JsonDerivedType(typeof(HybridSessionResumeInboundsResponse), typeDiscriminator: "HybridSessionResumeInboundsResponse")]
[JsonDerivedType(typeof(HybridSessionStartVoiceResponse), typeDiscriminator: "HybridSessionStartVoiceResponse")]
[JsonDerivedType(typeof(HybridSessionStopVoiceResponse), typeDiscriminator: "HybridSessionStopVoiceResponse")]
[JsonDerivedType(typeof(HybridSessionSetMicrophoneMuteResponse), typeDiscriminator: "HybridSessionSetMicrophoneMuteResponse")]
[JsonDerivedType(typeof(HybridSessionSetSoundMuteResponse), typeDiscriminator: "HybridSessionSetSoundMuteResponse")]
[JsonDerivedType(typeof(HybridSessionSetAfkResponse), typeDiscriminator: "HybridSessionSetAfkResponse")]
[JsonDerivedType(typeof(HybridSessionPokeResponse), typeDiscriminator: "HybridSessionPokeResponse")]
[JsonDerivedType(typeof(HybridSessionMovePeerResponse), typeDiscriminator: "HybridSessionMovePeerResponse")]


[JsonDerivedType(typeof(ErrorResponse), typeDiscriminator: "ErrorResponse")]
public abstract record BaseMessage
{
    /// <summary>
    /// The authenticated sender, stamped by the server gateway on incoming messages. Not named
    /// UserId: a positional UserId parameter of a derived record would bind to this property
    /// instead of declaring its own, and its value would be silently dropped. Never sent: the server
    /// knows who is on each connection, and messages it sends need no sender.
    /// </summary>
    [JsonIgnore]
    public int SenderId { get; set; }
}
