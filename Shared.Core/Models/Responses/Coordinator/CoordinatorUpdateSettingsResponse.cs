using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

/// <param name="Settings">The settings as they were saved.</param>
public record CoordinatorUpdateSettingsResponse(
    Guid RequestId,
    CoordinatorSettingsDTO Settings) : CoordinatorResponse(RequestId);
