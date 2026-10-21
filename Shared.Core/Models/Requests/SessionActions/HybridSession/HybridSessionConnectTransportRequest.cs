using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Models.Media.Transport;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionConnectTransportRequest(
    [property: EntityId] int SessionId) : HybridSessionRequest
{
    [Required]
    public required DTLSParameters DtlsParameters { get; init; }

    public HybridSessionConnectTransportResponse ToResponse(HybridSessionConnectTransportResult result)
        => new(RequestId, result);
}
