using Shared.Enums.Coordinator;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorSettingsResponse(
    Guid RequestId,
    CoordinatorSettingsResult Result,
    CoordinatorSettingsDTO? Settings,
    IList<SfuNodeDTO>? SfuNodes) : CoordinatorResponse(RequestId);
