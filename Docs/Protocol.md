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
{"$type":"KickFromSessionRequest","SessionId":1,"UserTargetId":2,"SessionType":2,"Reason":"spam","RequestId":"bc52bace-965d-4ee8-afa6-0b7164708bb6","SenderId":0}
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
uses that method, so the `RequestId` is always copied. Exceptions to the naming:
`EditTextMessageRequest` → `EditMessageResponse`, `ChangeHybridSessionSettingsRequest` →
`ChangeSessionSettingsResponse`. Requests that change one role are answered with
`CoordinatorRoleResponse`; assigning and unassigning a role are answered with
`CoordinatorUserRolesResponse`.

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
| Generic sessions | `Requests.SessionActions.Generic`, `Notifications.SessionInfo` | Create, join, leave, kick, ban, approve; categories and layout |
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
`UserAuthRequest`. `ServerMessage` then lists what the user can prove as names of
`SecondStageAuthType`, comma separated (`Password,TwoFactor,FIDO2`), and `FIDO2LoginOptionsDTO` is set
when the user has passkeys. The client finishes with either

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
