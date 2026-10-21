using System.Text.Json.Serialization;
using Shared.Enums;

namespace Shared.Models.Requests.SessionActions.Generic.Models.SessionData;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type",
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]

[JsonDerivedType(typeof(HybridSessionCreateData), typeDiscriminator: "VideoData")]
[JsonDerivedType(typeof(ChatSessionCreateData), typeDiscriminator: "ChatData")]
public abstract record CreateSessionData
{
    public abstract SessionType SessionType { get; init; }
}
