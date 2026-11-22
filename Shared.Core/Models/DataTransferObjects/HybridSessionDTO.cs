using Shared.Enums;
using Shared.Enums.WebRTC;

namespace Shared.Models.DataTransferObjects;

public class HybridSessionDTO : SessionDTO
{
    public required WebRTCEngine EngineType { get; set; }
    /// <summary>How many peers the session holds at most.</summary>
    public required int Capacity { get; set; }
    /// <summary>Joiners wait for someone with ApproveMembers; such users come in at once.</summary>
    public bool IsApprovalRequired { get; set; }
    public HybridSessionNodeState State { get; set; }
    public HybridMediaSettingsDTO Media { get; set; } = new();
}
