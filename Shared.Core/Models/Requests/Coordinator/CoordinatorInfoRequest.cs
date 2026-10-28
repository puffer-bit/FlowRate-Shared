using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;

namespace Shared.Models.Requests.Coordinator;

public record CoordinatorInfoRequest() : CoordinatorRequest
{
    public CoordinatorInfoResponse ToResponse(
        CoordinatorSessionDTO coordinatorSessionDTO) =>
        new (RequestId, coordinatorSessionDTO);
}
