using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Enums.WebRTC;
using Shared.Models.DataTransferObjects;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic.Models.SessionData;

/// <param name="Media">Null leaves the defaults of <see cref="HybridMediaSettingsDTO"/>.</param>
public record HybridSessionCreateData(
    [property: DefinedEnum] WebRTCEngine EngineType,
    [property: DefinedEnum] VideoResolutions Resolution = VideoResolutions.FHD,
    [property: Range(Limits.VideoBitrateMinKbps, Limits.VideoBitrateMaxKbps)] int? VideoBitrate = 2500,
    bool IsAudioTransferEnabled = false,
    bool IsDataChannelEnabled = false,
    bool IsSimulcastEnabled = false,
    bool IsTunnelingEnabled = false,
    bool IsProxyActive = false,
    HybridMediaSettingsDTO? Media = null
) : CreateSessionData
{
    public override SessionType SessionType { get; init; } = SessionType.Hybrid;
}
