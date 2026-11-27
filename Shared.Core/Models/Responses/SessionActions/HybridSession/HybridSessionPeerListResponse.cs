using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionPeerListResponse(
    Guid RequestId,
    Dictionary<int, PeerDTO> Peers) : HybridSessionResponse(RequestId);
