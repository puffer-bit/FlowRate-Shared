using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Enums.WebRTC;
using Shared.Models.DataTransferObjects;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic.Models.SessionData;

/// <param name="Capacity">How many peers the session holds at most.</param>
/// <param name="Media">Null leaves the defaults of <see cref="HybridMediaSettingsDTO"/>.</param>
public record HybridSessionCreateData(
    [property: DefinedEnum] WebRTCEngine EngineType,
    [property: Range(Limits.SessionCapacityMin, Limits.SessionCapacityMax)] int Capacity,
    HybridMediaSettingsDTO? Media = null
) : CreateSessionData
{
    public override SessionType SessionType { get; init; } = SessionType.Hybrid;
}
