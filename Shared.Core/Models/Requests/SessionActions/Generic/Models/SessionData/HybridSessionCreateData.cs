using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Enums.WebRTC;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic.Models.SessionData;

public record HybridSessionCreateData(
    [property: DefinedEnum] WebRTCEngine EngineType,
    [property: DefinedEnum] WebRTCVideoCodecs VideoCodecs = WebRTCVideoCodecs.H264,
    [property: DefinedEnum] WebRTCAudioCodecs AudioCodecs = WebRTCAudioCodecs.Opus,
    [property: DefinedEnum] VideoResolutions Resolution = VideoResolutions.FHD,
    [property: Range(Limits.VideoBitrateMinKbps, Limits.VideoBitrateMaxKbps)] int? VideoBitrate = 2500,
    bool IsAudioTransferEnabled = false,
    bool IsDataChannelEnabled = false,
    bool IsSimulcastEnabled = false,
    bool IsTunnelingEnabled = false,
    bool IsProxyActive = false,
    bool DisableStun = false,
    bool DisableTurn = false
) : CreateSessionData
{
    public override SessionType SessionType { get; init; } = SessionType.Hybrid;
}
