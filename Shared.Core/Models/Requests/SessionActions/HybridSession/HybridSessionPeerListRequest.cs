using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

public record HybridSessionPeerListRequest(
    [property: EntityId] int SessionId) : HybridSessionRequest
{
    public HybridSessionPeerListResponse ToResponse(Dictionary<int, PeerDTO> peers)
        => new (RequestId, peers);
}
