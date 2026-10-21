using System.ComponentModel.DataAnnotations;
using Shared.Enums.Coordinator;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Replaces the settings of the coordinator. Accepted only while <see cref="ExpectedRevision"/> is the current
/// revision, so a change made meanwhile elsewhere is never overwritten unseen.
/// </summary>
public record CoordinatorUpdateSettingsRequest(
    [property: Required] CoordinatorSettingsUpdateDTO Settings,
    long ExpectedRevision) : CoordinatorRequest
{
    public CoordinatorUpdateSettingsResponse ToResponse(CoordinatorSettingsResult result,
        CoordinatorSettingsDTO? settings = null, IDictionary<string, string[]>? errors = null)
        => new(RequestId, result, settings, errors);
}
