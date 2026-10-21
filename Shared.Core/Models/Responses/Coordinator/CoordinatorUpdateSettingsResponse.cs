using Shared.Enums.Coordinator;
using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

/// <param name="Settings">The settings as they are now: the saved ones, or on a conflict the newer ones.</param>
/// <param name="Errors">
/// Why values were refused, by field ("Heartbeat.TimeoutSeconds"; "" for the whole change).
/// </param>
public record CoordinatorUpdateSettingsResponse(
    Guid RequestId,
    CoordinatorSettingsResult Result,
    CoordinatorSettingsDTO? Settings,
    IDictionary<string, string[]>? Errors) : CoordinatorResponse(RequestId);
