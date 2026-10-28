using Shared.Enums;
using Shared.Enums.WebRTC;
using Shared.Models.DataTransferObjects;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic.Models.SessionData;

/// <param name="Media">Null leaves the defaults of <see cref="HybridMediaSettingsDTO"/>.</param>
public record HybridSessionCreateData(
    [property: DefinedEnum] WebRTCEngine EngineType,
    HybridMediaSettingsDTO? Media = null
) : CreateSessionData
{
    public override SessionType SessionType { get; init; } = SessionType.Hybrid;
}
