using Shared.Enums;
using Shared.Enums.WebRTC;

namespace Shared.Models.DataTransferObjects;

public class HybridSessionDTO : SessionDTO
{
    public required WebRTCEngine EngineType { get; set; }

    public bool IsHostConnected { get; set; }
    public bool IsAudioRequested { get; set; }
    public bool IsHostMustApprove { get; set; }
    public required int HostId { get; set; }
    public HybridSessionNodeState State { get; set; }
    public HybridMediaSettingsDTO Media { get; set; } = new();
}
