using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;

namespace Shared.Models.Requests.Coordinator;

public record CoordinatorBansRequest : CoordinatorRequest
{
    public CoordinatorBansResponse ToResponse(List<BanDTO> bans) => new(RequestId, bans);
}

public record CoordinatorRevokeBanRequest(Guid BanId) : CoordinatorRequest
{
    public CoordinatorRevokeBanResponse ToResponse() => new(RequestId);
}
