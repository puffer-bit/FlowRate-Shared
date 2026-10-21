using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionPeerListRequest(
    [property: EntityId] int SessionId) : HybridSessionRequest
{
    public HybridSessionPeerListResponse ToResponse(PeerListRequestResult result, Dictionary<int, PeerDTO>? peers = null)
        => new (RequestId, result, peers);
}
