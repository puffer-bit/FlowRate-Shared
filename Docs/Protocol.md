# Coordinator protocol

The client and the coordinator talk over one WebSocket, with JSON text messages. Every message is
a C# record in `Shared.Core`. This document describes how those records go over the wire, which
rules they follow, and what to check when you change them.

## Where it lives

| File | Role |
|------|------|
| `Shared.Core/Models/BaseMessage.cs` | Root of all messages; registers every concrete message with its `$type` |
| `Shared.Core/Models/Requests/Request.cs` | Base of requests; carries `RequestId` |
| `Shared.Core/Models/Responses/Response.cs` | Base of responses; echoes the `RequestId` |
| `Shared.Core/Models/Notifications/Notification.cs` | Base of messages the server sends on its own |
| `Shared.Core/Models/Responses/ErrorResponse.cs` | The answer to a request that failed |
| `Shared.Core/Enums/ErrorCode.cs` | Why a request failed |
| `Shared.Core/Serialization/ProtocolJsonContext.cs` | Source-generated JSON metadata, the only serializer either side uses |
| `Shared.Core/Validation/Limits.cs` | Size limits of all fields |
| `Shared.Core/Validation/ValidationAttributes.cs` | The protocol's own validation attributes |
| `Shared.Core/Validation/MessageValidator.cs` | Runs the attributes over a whole message |
| `Shared.Core/Protocol/ProtocolVersion.cs` | The version and its handshake headers |
| `Shared.Core/Models/Media/MediaEndpoints.cs` | The HTTP media endpoints of the coordinator |
| `Shared.Core/Models/Push/PushEndpoints.cs` | The HTTP push endpoints of the coordinator |
| `Shared.Tests/protocol-schema.txt` | Snapshot of the schema of the current version |

## Connection

```
client                                          coordinator
  │  GET wss://host:port/                            │
  │  FlowRate-Protocol: 1                            │
  │ ───────────────────────────────────────────────► │
  │                                                  │  version served?
  │ ◄─── 101 Switching Protocols ─────────────────── │  yes
  │ ◄─── 426 Upgrade Required ────────────────────── │  no:
  │        FlowRate-Protocol: <server Current>       │    both headers, no socket
  │        FlowRate-Protocol-Min: <server MinSupported>
  │                                                  │
  │  UserAuthRequest ──────────────────────────────► │
  │ ◄──────────────────────────── UserAuthResponse   │
  │  ... requests, responses, notifications ...      │
```

- The version travels in a header of the upgrade request, before any message. A change of the
  message format therefore cannot break the version check itself.
- `ProtocolVersion.IsSupported` accepts only a positive integer between `MinSupported` and
  `Current`. A missing header is never served.
- With a 426 the client can tell who is outdated: if its `Current` is below the server's
  `MinSupported`, the client needs an update; otherwise the server does
  (`Client.Application/Ports/ProtocolMismatch.cs`).
- Authentication is the first exchange on the socket: `UserAuthRequest`, then
  `TwoFactorUserAuthRequest` or `FIDO2UserAuthRequest` when the response says
  `SecondStageRequired`. A device with a live session token skips the second stage (see
  [Device sessions](#device-sessions)).
- While connected, the client sends a `HeartbeatPingRequest` every 5 seconds
  (`Client.Api/Protocol/HeartbeatHandler.cs`). Before a planned shutdown the server sends
  `HeartbeatServerClosingNotification` or `HeartbeatServerRestartingNotification`.

## Message format

Each WebSocket text message is one JSON object, a `BaseMessage`:

```json
{"$type":"KickFromSessionRequest","SessionId":1,"TargetUserId":2,"SessionType":2,"Reason":"spam","RequestId":"bc52bace-965d-4ee8-afa6-0b7164708bb6","SenderId":0}
```

| Rule | Detail |
|------|--------|
| `$type` comes first | It names the concrete record, exactly its C# type name. A reader rejects an object whose `$type` is not the first property |
| Unknown `$type` | Deserialization fails with a `JsonException`. A new message type therefore needs a new version, see [Versioning](#versioning) |
| Property names | PascalCase, as in C#. Reading ignores case |
| Unknown properties | Ignored when reading |
| Enums | Written as numbers, not names. Renumbering a member is a breaking change |
| Nesting | At most 16 levels (`MaxDepth` of the context) |
| `SenderId` | On every message. The server gateway overwrites it on incoming messages with the authenticated user; whatever the client puts there is ignored |

Why `SenderId` and not `UserId`: a derived record with a positional `UserId` parameter would bind
to the base property instead of declaring its own, and its value would be lost without a warning.
Do not add a property named like one of the base properties.

Serialization always goes through `ProtocolJsonContext.Default.BaseMessage`. The context is
source-generated: no reflection at run time, so it works under NativeAOT. The test
`SourceGeneratedJson_MatchesReflectionJson` makes sure it writes the same JSON as the reflection
serializer did before.

## Requests, responses and notifications

There are three kinds of message:

| Kind | Sent by | Correlation |
|------|---------|-------------|
| `Request` | client | `RequestId`, a new GUID for every request |
| `Response` | server, once per request | the `RequestId` of the request it answers |
| `Notification` | server, at any time | none |

Each request has exactly one response type. In most cases it is named the same with `Response`
instead of `Request`, and the request has a `ToResponse(...)` method that builds it. The server
uses that method, and `ToError(...)` for a failure, so the `RequestId` is always copied. Exceptions
to the naming: requests that change one role are answered with `CoordinatorRoleResponse`, assigning
and unassigning a role with `CoordinatorUserRolesResponse`, and `UserAuthRequest` also with
`ToSecondStageResponse(...)`.

A response is sent on success only and carries what the request asked for, without a result
code. A request that fails is answered with an `ErrorResponse` instead, with the same `RequestId`:

| Field | Meaning |
|-------|---------|
| `Code` | Why it failed, an `ErrorCode` |
| `Details` | What went wrong, for logs; not meant for end users |
| `Fields` | Why values were refused, by field (`"Heartbeat.TimeoutSeconds"`; `""` for the request as a whole), with `InvalidRequest` and `InvalidValue` |

`ErrorCode` is one list for the whole protocol, grouped by hundreds; the request it answers tells what
a code is about (`SessionNotFound` of a join is the session to join):

| Codes | Group |
|-------|-------|
| 1–99 | The request as a whole: `Internal`, `TimedOut` (the server did not get the work done in time), `RateLimited`, `InvalidRequest` (fails `MessageValidator`), `InsufficientPermissions` (see `Enums/Permission.cs`), `UserFrozen`, `NotConnected`, `FeatureDisabled` |
| 100–199 | Logging in: `AuthFailed`, `Banned`, `OnlineLimitReached`, `UserLimitReached` |
| 200–299 | What the request is about does not exist: `UserNotFound`, `SessionNotFound`, `PeerNotFound`... |
| 300–399 | The state of things does not allow it: `Conflict`, `NameTaken`, `SessionFull`, `NotAdmitted`, `CannotTargetSelf`, `HierarchyViolation`... |
| 400–499 | A value the format allows but the server cannot use: `InvalidValue`, the attachment codes, `UnsupportedMedia` |
| 500–599 | Account security: `ProofRequired`, `VerificationFailed` |
| 600–699 | Media servers: `SFUError`, `SFUUnavailable` |

`Conflict` means someone else changed the settings or the role after the revision the change was
based on; the client fetches them again and the user decides anew.

A client must therefore expect either the regular response or `ErrorResponse` for every request. The
FlowRate client turns the latter into an exception that carries the `ErrorCode`
(`Client.Api/Protocol/ResponseAwaiter.cs`), and gives up on a request after 40 seconds without a
response. Giving up is the client's own matter and is not an `ErrorCode`.

## Message areas

| Area | Namespace (`Shared.Models.…`) | Content |
|------|-------------------------------|---------|
| Auth | `Requests.Auth`, `Responses.Auth` | Login, second stage by 2FA code or FIDO2 |
| Coordinator | `Requests.Coordinator`, `Notifications.CoordinatorInfo` | Server info, users, profile, settings, roles, push tokens, media token |
| Account security | `Requests.Coordinator.Security`, `Responses.Coordinator.Security` | The sender's password, one-time codes and passkeys |
| Heartbeat | `Requests.Heartbeat`, `Notifications.Heartbeat` | Ping, server closing or restarting |
| Generic sessions | `Requests.SessionActions.Generic`, `Notifications.SessionInfo` | Create, join, leave, kick, approve; categories and layout |
| Chat sessions | `Requests.SessionActions.ChatSession` | History, paging, changes since a revision, send, edit, delete, read state |
| Hybrid sessions | `Requests.SessionActions.HybridSession` | Voice and video rooms on an SFU: transport, voice, camera, screen share, mute |

Joining a hybrid session gives `TransportData` (ICE parameters and candidates, DTLS parameters) in
`HybridSessionJoinResponse`. The client answers with its own DTLS parameters in
`HybridSessionConnectTransportRequest`. `HybridSessionJoinRequest.ClientEpoch` identifies the
client's DTLS context: while it stays the same, the SFU reuses the transport it has.

Every `Outbound` carries the payload type and SSRC the client sends the track with. When
`RtxPayloadType` is not 0, the client answers NACKs of the SFU for that track by resending the lost
packets as RTX (RFC 4588) with `RtxPayloadType` and `RtxSSRC`; the SFU knows no other SSRC. An
`Inbound` has no RTX stream: the SFU resends a lost packet as it was, on the same SSRC, so the client
only has to ask for it with a NACK, and only when `IsNackEnabled` says the SFU keeps the packets of
the track. `Inbound.JitterBufferMs` is how long the client buffers the track. An Opus `Outbound`
says how the client encodes it: `IsFecEnabled`, `FecLossPercent` and `IsDtxEnabled`.

A hybrid session carries its media settings in `HybridSessionDTO.Media` (`HybridMediaSettingsDTO`):
the codecs of camera, screen and audio, NACK and RTX of video, NACK of audio, Opus FEC and DTX, the
jitter buffers of receivers and the keyframe request delay of the SFU. They are given on creation
(`HybridSessionCreateData.Media`, defaults when null) and changed with
`ChangeHybridSessionSettingsRequest`. Only `SupportedVideoCodecs` and `SupportedAudioCodecs` are
accepted. Bitrate, frame rate and resolution are not session settings: the sender picks them when a
stream starts. The settings apply to tracks started after a change: the coordinator gives them
to the SFU and puts them into each `Outbound` and `Inbound`, which the client follows. A client
that receives a track does not read the session settings itself.

## Logging in and account security

A user who has a password, one-time codes (TOTP) or passkeys is answered `SecondStageRequired` to
`UserAuthRequest`. `SecondStages` then lists what the user can prove (`SecondStageAuthType`), and
`FIDO2LoginOptions` is set when the user has passkeys. The client finishes with either

- `TwoFactorUserAuthRequest`: the password and the code, each when the user has it, or
- `FIDO2UserAuthRequest`: a passkey assertion. A passkey alone is enough.

Binary values of the passkey messages (challenge, credential ids, the parts of the assertion) are
base64url. The user handle of a passkey is the user id as UTF-8 text. User verification is always
required. The server gives three tries and two minutes for the second stage; a code is good for one
login.

A login that fails is answered with an `ErrorResponse`: `AuthFailed`, `Banned`, `OnlineLimitReached`
or `UserLimitReached`.

Once logged in, the client sets up the sender's own login with the `User*` requests of
`Requests.Coordinator.Security`. Each answers with `UserSecurityDTO`, what the user has now.

| Request | Does |
|---------|------|
| `UserSecurityInfoRequest` | Tells what the user has |
| `UserSetPasswordRequest` | Sets the password (8–128 characters), or removes it with `NewPassword` null |
| `UserTwoFactorSetupRequest` | Offers a secret for an authenticator app (`Secret`, `otpauth://` `Uri`) |
| `UserTwoFactorEnableRequest` | Turns codes on with a code made from the offered secret |
| `UserTwoFactorDisableRequest` | Turns codes off |
| `UserPasskeyOptionsRequest` | WebAuthn creation options (`PublicKeyCredentialCreationOptionsJSON`) |
| `UserPasskeyRegisterRequest` | Registers the passkey made for those options (`RegistrationResponseJSON`) |
| `UserPasskeyDeleteRequest` | Removes a passkey |

Every request that changes something or starts a setup carries `UserSecurityProofDTO`: the current
password and one-time code, each when the user has it. A missing or wrong proof is answered
`ProofRequired`, so an open session alone cannot change how the account logs in. The control panel can
set up and take away all of it too, for a user who lost a device.

## Device sessions

The identity (`#` and a GUID, given by the server to a new user) stays the key of the account: the
client keeps it, and it logs in from any device. The server keeps only its SHA-256.

- Every successful login, by identity or by token, answers with `SessionToken` (in `UserAuthResponse`,
  `TwoFactorUserAuthResponse` or `FIDO2UserAuthResponse`): 32 random bytes, base64url. The client
  keeps it with the identity and sends it with the next `UserAuthRequest`, along with `UserIdentity`
  and `Device` (`DeviceInfoDTO`: name, platform, client version).
- A token the server knows logs the device in at once, without the second stage, and is answered with
  the next token. The token before stays valid until the next one is used, for a device that lost
  the answer. `UserIdentity` is null in that response: the client keeps its own.
- An unknown or expired token is not refused: the server goes on with `UserIdentity`, second stage
  included, and starts a new session.
- A session ends after 90 days without a login, or when it is ended. The server keeps only the
  hashes of the tokens.

Requests of the sender (`Requests.Coordinator.Security`, answered with `UserSessionsResponse` or
`UserIdentityResetResponse`):

| Request | Does |
|---------|------|
| `UserSessionsRequest` | The sessions of the user, the latest used first; `IsCurrent` marks the asking one |
| `UserSessionRevokeRequest` | Ends one session (not the current one), or every other one with `SessionId` null; no proof asked |
| `UserIdentityResetRequest` | Gives a new identity (with `UserSecurityProofDTO`) and ends every other session |

The connections of an ended session get `CoordinatorUserSessionEndedNotification` (`Revoked` or
`IdentityReset`) and are closed. The client drops its token and does not reconnect on its own.

## Validation

Every request field that comes from the user has a limit, and the client and the server check
the same limits:

- The constants are in `Limits`. String limits match the server database columns.
- The rules are standard `System.ComponentModel.DataAnnotations` attributes (`Required`,
  `StringLength`, `Range`) together with the protocol's own:

| Attribute | Accepts |
|-----------|---------|
| `EntityId` | An id of an existing entity: `1` or more |
| `DefinedEnum` | A declared member of the enum; blocks values like `(SessionType)99` |
| `NoControlCharacters` | Text without control characters; optionally with line breaks and tabs |
| `NotBlank` | Text that is not empty or whitespace |
| `HttpsUrl` | An absolute `https` URL |
| `GuidString` | A GUID written as text |
| `DTLSFingerprint` | Colon-separated hex pairs |
| `HexBytes(n)` | Exactly `n` bytes in hex |
| `Base64Bytes(n)` | Exactly `n` bytes in base64; overlong input is refused without decoding |
| `OneOf(...)` | One of a fixed set of strings, ignoring case |
| `ItemCount(min, max)` | A collection with that many items |

Use `ItemCount` for collections, never `Length` or `MaxLength`: those read `Count` by reflection
and are not trim-safe.

`MessageValidator.TryValidate` walks a message through the metadata of `ProtocolJsonContext`, not
by reflection. It checks the attributes of every property, then nested objects, collection items
and dictionary values, up to 8 levels deep. A type that implements `IValidatableObject` gets its
object-level rules checked once all its properties are valid; `NewChatMessageModel` uses this,
for example for "text or an attachment".

The client validates before sending (`Client.Api/CoordinatorGateway.Requests.cs`). An invalid
request then fails at once and never reaches the server, which would answer `InvalidRequest`.

## HTTP endpoints

Some traffic does not fit the socket. The coordinator serves it over HTTP on the same address and
port.

**Media** (`MediaEndpoints`). Every request carries the media token as a bearer token. The client
gets the token with `MediaTokenRequest`; it is short-lived and requested again when it expires or
an endpoint answers 401.

| Path | Use |
|------|-----|
| `/media/uploads` | Uploads by the tus protocol 1.0.0. `Upload-Metadata` keys are in `MediaUploadMetadata` (`chatId`, `mimeType`, `kind`) |
| `GET /media/images/{id}?variant=original\|preview` | An image of a chat message; byte ranges supported |
| `GET /media/avatars/{id}` | A user avatar; a new avatar has a new id, so a fetched one never changes |

**Push** (`PushEndpoints`). A device answers a notification without a connection, using the reply
token it got on registering. Bodies are serialized with the same context.

| Path | Body | Use |
|------|------|-----|
| `POST /push/reply` | `PushReplyRequest` | Sends a text message to a chat |
| `POST /push/read` | `PushMarkReadRequest` | Moves the read position, like "Mark as read" |

Both answer 204 on success, 400 for an invalid body, 401 for an unknown token, 403 when the user
may not write there, 404 for a chat that does not exist and 429 for too many requests.

## Versioning

`ProtocolVersion.Current` must change whenever old clients or servers can no longer read the
messages: a field removed or renamed, a type changed, an enum member renumbered, a request old
servers do not know. `MinSupported` is the oldest client version the server still serves.

The current protocol version and minimum supported version are both 6. Clients using earlier
versions are rejected during the WebSocket upgrade with 426 Upgrade Required.

So that no such change goes unnoticed, `ProtocolSchemaTests` describes the whole schema as text.
The description contains every type reachable from `BaseMessage`, `PushReplyRequest` and
`PushMarkReadRequest`, with property types, `$type` discriminators and enum numbers. The test
compares it with `Shared.Tests/protocol-schema.txt`:

```
version 1
ApproveUserRequest.SessionId: Int32
BaseMessage <- "ApproveUserRequest": ApproveUserRequest
SessionType = enum Undefined=0, Chat=1, Hybrid=2, Voice=3
...
```

When the schema differs, the test fails and writes `protocol-schema.received.txt` next to the
snapshot (git ignores it). Then:

1. Decide whether the change is breaking. Adding an optional field that old readers can ignore
   usually is not. Adding a message old servers cannot read is.
2. If it is, raise `Current`. Raise `MinSupported` too once the server stops serving the old
   version.
3. Replace `protocol-schema.txt` with the received file and commit it with the change.

## Adding a message

1. Write the record in the folder of its area. Derive from the area's base (`CoordinatorRequest`,
   `HybridSessionRequest`, ...). For a request, add a `ToResponse(...)` method.
2. Register it in `BaseMessage.cs` with
   `[JsonDerivedType(typeof(X), typeDiscriminator: nameof(X))]`. `BaseMessageRegistrationTests`
   fails for an unregistered concrete type, a discriminator that is not the type name, or a
   duplicate.
3. Put limits on every string and collection of a request, including nested DTOs and every
   derived type a polymorphic property can hold. `EveryStringAndCollection_InARequest_HasASizeLimit`
   finds the ones you missed. Add `EntityId` to ids and `DefinedEnum` to enums.
4. Add typical valid and invalid instances to `MessageValidatorTests` when the rules are not
   trivial.
5. Run the tests and handle the schema snapshot as described in [Versioning](#versioning).
6. Update the client submodule and implement the server side. Both must be built from the same
   Shared commit.

## Typing activity

`ChatTypingRequest(ChatId, IsTyping)` is acknowledged by `ChatTypingResponse`. The authenticated
sender must be connected and have ViewSessions and SendMessages in that chat. Activity is sent
only to users who can view the chat, as `ChatSessionUserTypingNotification(ChatId, UserId, IsTyping)`.
It is transient: no database writes, history revisions or push notifications. Clients send an active
pulse at most once every two seconds while the input changes, then send false after five seconds
without changes, when the input is cleared, or when closing the chat. Switching chat views does not stop or renew activity. Receivers also expire activity
five seconds after the last pulse, clear it on an incoming message from that author, and ignore
activity from their own user (including other devices). Protocol version 4 requires both sides updated.

## Message reactions

`SetMessageReactionRequest(ChatId, MessageId, Emoji, IsAdded)` sets or removes the authenticated
sender's reaction and is acknowledged by `SetMessageReactionResponse`. The reaction palette is
`ChatReactionEmoji.All`. The sender needs ViewSessions and SendMessages in that chat. Archived
and read-only chats and system, deleted or read-only messages reject changes. Repeating the same
state does not write a revision or broadcast another update. A user can react with multiple emoji.

Changes persist in the existing reaction rows, advance the chat revision and broadcast the existing
`ChatSessionMessageEditedNotification` with the full message. They do not alter the text or EditTime.
History and incremental synchronization include reactions as before; no database migration is needed.

The protocol version is intentionally unchanged for this extension. Updated clients and servers are
required to set reactions. Older clients can still read messages and existing update notifications
from updated servers; older servers do not support the new request.


### Pinned messages and history windows (protocol 5)

`SetMessagePinnedRequest(ChatId, MessageId, IsPinned)` requires `ViewSessions` and
`ManageMessages` in the chat. Repeating the current state is harmless; at most 100 messages
may be pinned in a chat. Archived chats and system messages cannot be pinned. Read-only
chats still allow moderators to manage pins. `PinnedAt` (UTC) and `PinnedByUserId` describe
the current pin; both are null after unpinning. Pin changes increment the chat revision and
use the existing message-edited notification, without changing `EditTime` or sending a push.
Deleting a message clears its pin.

`ChatPinnedMessagesRequest(ChatId)` returns the full independently loaded pin list and its
revision. `ChatHistoryWindowRequest(ChatId, MessageId, Direction, Take)` returns up to `Take`
live messages in ascending ID order. `Around` includes the live target, with half the page
before it and the remaining messages after it. `Before` and `After` exclude the cursor and
can continue from a soft-deleted cursor. All targets and cursors must belong to the chat.
`HasOlder` and `HasNewer` describe the returned page boundaries. Both reads require view
access. Window responses are serialized with writes in that chat, so revision and boundaries
refer to the same state.

Clients keep a historical window separate from their contiguous cached tail. Live messages
beyond an unfilled gap update the cache and unread indicator; they are not appended to the
historical window. Returning to latest replaces the visible window with the current tail.

## Chat commands

When a new message starts with `/roll` or `/flip`, the server validates and executes the command
once, before persistence and the existing message-added notification. `/roll` defaults to 1–100;
`/roll minimum-maximum` accepts inclusive bounds from 1 to 1000. `/flip` has no arguments.
Invalid arguments return `InvalidValue` without storing a message. Unknown commands remain text.

`ChatTextContentDTO.CommandResult` is optional, server-owned metadata (`Kind`: Roll=1, Flip=2;
`Minimum`, `Maximum`, `Value`). Flip uses 0=heads, 1=tails. Client-supplied results are ignored.
`Text` contains a readable fallback with the resolved result for older clients, replies and exports.
Command messages cannot be edited; deletion, replies, attachments, pins and reactions keep their
existing behavior. History and cache preserve the outcome. Clients animate only a newly received
message, once, and show the stored outcome immediately for history, sync and pinned copies.
This optional field is backward compatible and does not change the protocol version.

### Personal chats and groups (protocol 5)

`ChatSessionDTO.Kind` distinguishes `ServerChannel` (0), `Direct` (1), and `Group` (2).
Personal conversations belong to the coordinator and never enter the shared session category layout.
`MemberIds` is persistent membership, independent of online presence and viewer subscriptions.
Only members receive the conversation, its messages, read state, typing, or media. Server role
administration does not grant access to a personal conversation.

Use `CreateSessionRequest` with `ChatSessionCreateData.Kind` and `MemberIds` for conversations.
The authenticated sender is included automatically. A direct conversation has exactly two members;
creation for an existing unordered pair returns that conversation, including for simultaneous requests.
Groups initially require at least two members and allow up to 100. Group names need not be unique.
Creation of personal conversations does not require `CreateSessions`.

`ChangeGroupRequest` replaces the name, membership and owner, and requires the current group owner.
The new owner must be a member. `Leave = true` instead removes the sender; an owner must transfer
ownership or delete the group first. Added members see the existing history. Removing a member
immediately revokes access and removes the conversation from that member's live list.

`ChatSessionDTO.LastMessage` contains the latest nondeleted message, or `null` for an empty chat.
It is included in the initial session list and refreshed through `ChatSessionReconfiguredNotification`
when the latest message is sent, edited, or deleted. Deletion restores the previous nondeleted message.
Consumers can render text or attachment previews without fetching each chat's history. The last
message carries its author, sent time, ID and revision using the existing message DTO.

These changes extend the protocol 5 development branch; `Current` and `MinSupported` remain 5.

## System chat messages

System events use the existing `ChatMessageDTO`, history, incremental revisions, last-message preview
and `ChatSessionMessageAddedNotification`. `IsSystem` and `IsReadOnly` are true; the sender is the
server system user (-1). Clients cannot create these messages through `NewChatMessageModel`.

Optional `SystemContent` contains `Kind` (`MemberAdded`=1, `MemberRemoved`=2, `MemberLeft`=3,
`CallStarted`=4, `CallEnded`=5), `ActorUserId`, `SubjectUserId` and `CallId`. Member events identify
the affected user; call events identify the call by GUID. A server-ended call may have no actor.
Clients should localize the structured event; `TextContent.Text` supplies a readable fallback.
Older stored system messages may have no `SystemContent`.

Successful group membership changes append one message per added/removed member, or a
`MemberLeft` message for voluntary departure. Membership and messages commit atomically.
Unchanged membership and rejected changes create no messages. Initial group creation does not
append membership-change messages. Removed users lose access before notifications are delivered.
System messages cannot be edited, deleted, pinned or reacted to and do not generate message push
notifications. Read-only chat settings do not suppress server events.

The server-only `IChatSessionManager.AddCallSystemMessageAsync` entry point records call start/end
events using the same storage and delivery path. The call lifecycle must invoke it after a successful
transition and ensure each transition is recorded once; direct calls invoke this entry point as described below.

`SystemContent` is an optional, backward-compatible field; protocol version remains 5.


## Direct audio calls (protocol 6)

`DirectCallRequest(ChatId, CallId, Action)` supports Start, Accept, Decline and End. The caller generates
one nonempty GUID and reuses it on retries. Calls are supported only in Direct chats, and require
membership independently of server roles. The caller must be connected; the recipient may be offline. Both must be free of another call or
voice session. A competing start returns Conflict. Invitations expire after 45 seconds.

`DirectCallResponse.Call` and `DirectCallChangedNotification.Call` carry Ringing, Active or Ended,
creation/start timestamps and the outcome (Completed, Declined, Cancelled, Missed or Disconnected).
Only the callee can accept or decline; cancellation and hangup require the selected caller/callee
device. Accepting on another device after an answer returns Conflict. Recent completed call IDs
are retained for harmless retries. Notifications go only to the two members, with IsSelectedDevice
computed separately per connection. Connection identifiers never cross the protocol boundary.

Accepting creates a runtime hybrid media session under the chat ID. It is omitted from channel lists
and uses the existing SFU/WebRTC transport. Only the two selected connections may join or send media
requests. Video and screen sharing are refused. Leaving, disconnecting the selected device or a
graceful server shutdown closes the call and releases the media session. Calls are transient and do
not resume after a coordinator connection is lost. Losing an SFU node follows existing media recovery.

Clients show incoming calls in FlowRate's notification system with Accept and Decline actions.
Answering, cancellation or timeout dismisses the invitation on all devices. Only the recipient hears
the ringing signal, which respects notification muting and repeats until the invitation resolves. Android also receives encrypted high-priority
FCM data pushes on every registered recipient device, including when disconnected. Call pushes carry
callId, callState, recipientId and expiresAt, with a 45-second transport TTL. Android shows a system call
notification with Accept and Decline actions while the app is in the background. An action opens the app,
reconnects the saved recipient account using its normal authentication/certificate flow, and submits the
call action over the authenticated connection. Active/Ended pushes dismiss the invitation; expired or
reordered invitations are ignored. Loading the session list also restores a pending invitation for the
reconnected recipient. Push delivery requires configured FCM/relay and a registered device; offline
calling otherwise times out as missed. Existing Android ongoing-call foreground notifications remain in use.

Accepted calls append CallStarted once. Every finished call, including unanswered invitations, appends
CallEnded with optional CallEndReason and CallDurationSeconds. Duration is present for accepted calls.
The DirectCallOutcomes server migration adds nullable outcome and duration columns to ChatMessages;
existing system messages remain readable. Revisions and privacy remain unchanged. Clients localize the stored outcome.
Protocol 6 requires updating both client and coordinator.
