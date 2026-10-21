using System.Text.Json.Serialization;
using Shared.Models;
using Shared.Models.Push;

namespace Shared.Serialization;

/// <summary>
/// Source-generated serialization metadata for the wire protocol. Derived messages and DTOs are
/// reached through the <see cref="JsonDerivedTypeAttribute"/> registrations, so nothing here needs
/// reflection at run time and it works under NativeAOT.
/// </summary>
// Messages nest a few levels at most; deeper input is malformed or hostile
[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true, MaxDepth = 16)]
[JsonSerializable(typeof(BaseMessage))]
[JsonSerializable(typeof(PushReplyRequest))]
[JsonSerializable(typeof(PushMarkReadRequest))]
public sealed partial class ProtocolJsonContext : JsonSerializerContext;
