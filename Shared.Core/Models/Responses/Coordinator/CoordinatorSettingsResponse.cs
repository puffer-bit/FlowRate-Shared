using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorSettingsResponse(
    Guid RequestId,
    CoordinatorSettingsDTO Settings,
    IList<SfuNodeDTO> SfuNodes) : CoordinatorResponse(RequestId);
