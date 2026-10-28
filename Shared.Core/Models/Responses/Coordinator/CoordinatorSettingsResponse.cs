using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorSettingsResponse(
    Guid RequestId,
    CoordinatorSettingsDTO Settings,
    IList<SFUNodeDTO> SFUNodes) : CoordinatorResponse(RequestId);
