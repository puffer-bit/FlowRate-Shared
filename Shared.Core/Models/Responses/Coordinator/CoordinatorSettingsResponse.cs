using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

/// <param name="DatabaseProvider">Read-only server database name (SQLite or PostgreSQL); null on earlier servers.</param>
public record CoordinatorSettingsResponse(
    Guid RequestId,
    CoordinatorSettingsDTO Settings,
    IList<SFUNodeDTO> SFUNodes,
    string? DatabaseProvider = null) : CoordinatorResponse(RequestId);
