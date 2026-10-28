using Shared.Enums;
using Shared.Models.Media;

namespace Shared.Models.DataTransferObjects;

public class PeerDTO
{
    public int UserId { get; init; }
    public bool IsAudioRequested { get; set; }
    public bool IsNegotiated { get; set; }
    public bool IsConnected { get; set; }
    public bool IsApproved { get; set; }
    public bool IsRejected { get; set; }
    public bool IsScreenShareActive { get; set; }
    public bool IsScreenAudioActive { get; set; }
    public bool IsVideoShareActive { get; set; }
    public bool IsMicrophoneMuted { get; set; }
    public bool IsSoundMuted { get; set; }
    public bool IsAfk { get; set; }
    public HybridSessionPeerState State { get; set; }
    public string? AfkMessage { get; set; }
    /// <summary>The users whose screen share this peer watches.</summary>
    public List<int> WatchedScreenShares { get; init; } = new();

    public Dictionary<string, Outbound> Outbounds { get; init; } = new();
    public Dictionary<string, Inbound> Inbounds { get; init; } = new();
}
