using Shared.Enums.Coordinator;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;

namespace Shared.Models.Requests.Coordinator;

/// <summary>The stored settings of the coordinator and the SFU nodes it can be given.</summary>
public record CoordinatorSettingsRequest() : CoordinatorRequest
{
    public CoordinatorSettingsResponse ToResponse(CoordinatorSettingsResult result,
        CoordinatorSettingsDTO? settings = null, IList<SfuNodeDTO>? sfuNodes = null)
        => new(RequestId, result, settings, sfuNodes);
}
