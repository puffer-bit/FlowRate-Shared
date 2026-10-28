# FlowRate Shared

The protocol types that the FlowRate client and the coordinator server share. Every message that
goes over the coordinator WebSocket is defined here once, together with its size limits and
validation rules. Both sides compile against the same code, so they cannot disagree about a field
name or a limit.

The client includes this repository as the `src/Shared` submodule.

## Projects

| Project | Contents |
|---------|----------|
| `Shared.Core` | Messages, DTOs, enums, the JSON context, validation and the protocol version |
| `Shared.Tests` | Checks the message registrations, the validation rules and the schema snapshot |
| `Shared.Parsers` | Empty for now; the client still references it |
| `Shared.Tools` | Only the obsolete `SdpParser`. The client builds its SDP itself (`Client.GStreamer.Services.Sdp.SdpBuilder`) |

`Shared.sln` contains Core, Parsers and Tests. Shared.Tools is only in the client solution.

All projects target `net10.0`. The libraries set `IsAotCompatible`: nothing in them uses
reflection at run time, so the client can be published with NativeAOT.

## Layout of Shared.Core

| Folder | Role |
|--------|------|
| `Models/BaseMessage.cs` | Root of every message; holds the `$type` registrations of all of them |
| `Models/Requests`, `Models/Responses`, `Models/Notifications` | The messages, grouped by area: `Auth`, `Coordinator`, `Heartbeat`, `SessionActions` (`Generic`, `ChatSession`, `HybridSession`), `WebRTC` |
| `Models/DataTransferObjects` | Objects that messages carry: users, roles, sessions, chat messages and their content |
| `Models/Media` | The HTTP media endpoints, tus upload metadata, and the ICE/DTLS transport data of the SFU |
| `Models/Push` | The HTTP push endpoints and their request bodies |
| `Enums` | The error codes of requests, permissions, session and peer states |
| `Serialization/ProtocolJsonContext.cs` | Source-generated System.Text.Json metadata |
| `Validation` | `Limits`, the validation attributes and `MessageValidator` |
| `Protocol/ProtocolVersion.cs` | The protocol version and its handshake headers |

## Usage

Serialize and deserialize through the generated context, always as `BaseMessage`. That way the
`$type` discriminator is written and read:

```csharp
var json = JsonSerializer.Serialize<BaseMessage>(request, ProtocolJsonContext.Default.BaseMessage);
var message = JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage);
```

Validate a request before sending it. The server runs the same check:

```csharp
if (!MessageValidator.TryValidate(request, out var errors))
    // do not send; errors name the failing fields
```

Most requests have a `ToResponse(...)` method that builds their response with the right
`RequestId`. The server uses it.

## Building and testing

```bash
dotnet build Shared.sln
dotnet test Shared.Tests/Shared.Tests.csproj
```

The client CI (`.github/workflows/tests.yml` in FlowRate-Client) runs Shared.Tests on Linux and
Windows on every push.

## Changing the protocol

Read [Docs/Protocol.md](Docs/Protocol.md) first. In short:

1. Add the message type and register it in `BaseMessage` with `[JsonDerivedType]`, using its own
   type name as the discriminator.
2. Put a size limit on every string and collection of a request (`StringLength`, `ItemCount` or
   `OneOf`), and the matching constant in `Limits`.
3. Run the tests. `ProtocolSchemaTests` fails and writes `protocol-schema.received.txt`. If old
   clients or servers cannot read the new schema, raise `ProtocolVersion.Current`. Then replace
   `protocol-schema.txt` with the received file.

## License

MIT, see [LICENSE](LICENSE).
