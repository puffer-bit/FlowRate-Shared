using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Models.Media.Transport;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionJoinRequest(
    [property: EntityId] int SessionId
) : HybridSessionRequest
{
    /// <summary>
    /// Identifies the DTLS context of the client. The SFU reuses an existing transport while
    /// this value stays the same and recreates it once the client has rebuilt its own side.
    /// </summary>
    [StringLength(Limits.ClientEpochMaxLength), GuidString]
    public string? ClientEpoch { get; init; }

    public HybridSessionJoinResponse ToResponse(HybridSessionJoinResult result, TransportData? transportData = null)
        => new(RequestId, result, transportData);
}
