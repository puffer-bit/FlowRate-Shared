using Shared.Enums;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.SessionActions.HybridSession;

public record HybridSessionPeerListResponse(
    Guid RequestId,
    PeerListRequestResult Result,
    Dictionary<int, PeerDTO>? Peers) : HybridSessionResponse(RequestId);
