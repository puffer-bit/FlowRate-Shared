using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorInfoResponse(
    Guid RequestId,
    CoordinatorSessionDTO CoordinatorSessionDto) : CoordinatorResponse(RequestId);
