using Shared.Enums;
using System.ComponentModel.DataAnnotations;
using Shared.Validation;

namespace Shared.Models.DataTransferObjects;

/// <summary>
/// Stored settings of the coordinator, as the control panel edits them. The server password is never sent,
/// only whether there is one.
/// </summary>
public class CoordinatorSettingsDTO
{
    /// <summary>Grows with every saved change; a change is accepted only against the current one.</summary>
    public long Revision { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty;
    public int Port { get; set; }
    public bool IsPasswordProtected { get; set; }

    public int MaxOnlineUsers { get; set; }
    public int MaxUsers { get; set; }

    public bool IsMotdEnabled { get; set; }
    public string? MotdText { get; set; }

    public List<string> AllowedIps { get; set; } = [];
    public List<string> BannedIps { get; set; } = [];

    public int HeartbeatIntervalSeconds { get; set; }
    public int HeartbeatTimeoutSeconds { get; set; }

    public long MaxFileSizeBytes { get; set; }

    /// <summary>0 when the media storage is not limited.</summary>
    public long StorageQuotaBytes { get; set; }

    /// <summary>How many uploads one user may keep before attaching them to a message.</summary>
    public int MaxUnattachedUploadsPerUser { get; set; }

    /// <summary>How long an upload not attached to a message is kept.</summary>
    public int UnattachedUploadLifetimeHours { get; set; }

    /// <summary>How long a token for media requests stays valid.</summary>
    public int MediaTokenLifetimeMinutes { get; set; }

    public CoordinatorRateLimitsDTO RateLimits { get; set; } = new();

    public List<int> SfuNodeIds { get; set; } = [];
}

/// <summary>
/// The whole new settings of the coordinator. Values are checked by the server, which answers with the reasons
/// it refused them. A changed address or port restarts the coordinator, which disconnects its users.
/// </summary>
public class CoordinatorSettingsUpdateDTO
{
    [Required, StringLength(Limits.CoordinatorNameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(Limits.IpAddressMaxLength)]
    public string Ip { get; set; } = string.Empty;

    public int Port { get; set; }

    /// <summary>Sets a new server password. Null keeps the current one.</summary>
    [StringLength(Limits.PasswordMaxLength)]
    public string? NewPassword { get; set; }

    /// <summary>Lets users in without a server password.</summary>
    public bool RemovePassword { get; set; }

    public int MaxOnlineUsers { get; set; }
    public int MaxUsers { get; set; }

    public bool IsMotdEnabled { get; set; }

    [StringLength(Limits.MotdTextMaxLength)]
    public string? MotdText { get; set; }

    [Required, ItemCount(Limits.IpListMaxCount)]
    public List<string> AllowedIps { get; set; } = [];

    [Required, ItemCount(Limits.IpListMaxCount)]
    public List<string> BannedIps { get; set; } = [];

    public int HeartbeatIntervalSeconds { get; set; }
    public int HeartbeatTimeoutSeconds { get; set; }

    public long MaxFileSizeBytes { get; set; }
    public long StorageQuotaBytes { get; set; }

    public int MaxUnattachedUploadsPerUser { get; set; }
    public int UnattachedUploadLifetimeHours { get; set; }
    public int MediaTokenLifetimeMinutes { get; set; }

    [Required]
    public CoordinatorRateLimitsDTO RateLimits { get; set; } = new();

    [Required, ItemCount(Limits.SfuNodesMaxCount)]
    public List<int> SfuNodeIds { get; set; } = [];
}

/// <summary>At most <see cref="Count"/> requests per <see cref="PeriodSeconds"/>.</summary>
public class RequestRateDTO
{
    public int Count { get; set; }
    public double PeriodSeconds { get; set; }
}

/// <summary>
/// How many requests and connections the coordinator takes from one client. Every request falls into one group
/// and also counts against <see cref="Global"/>.
/// </summary>
public class CoordinatorRateLimitsDTO
{
    /// <summary>Every request of one connection.</summary>
    [Required] public RequestRateDTO Global { get; set; } = new();

    /// <summary>Logins from one IP address, over all its connections.</summary>
    [Required] public RequestRateDTO AuthenticationPerIp { get; set; } = new();

    [Required] public RequestRateDTO SessionManagement { get; set; } = new();
    [Required] public RequestRateDTO SessionLayout { get; set; } = new();
    [Required] public RequestRateDTO SessionMedia { get; set; } = new();
    [Required] public RequestRateDTO MicrophoneMute { get; set; } = new();
    [Required] public RequestRateDTO InboundResume { get; set; } = new();
    [Required] public RequestRateDTO Moderation { get; set; } = new();
    [Required] public RequestRateDTO ChatWrite { get; set; } = new();
    [Required] public RequestRateDTO ChatRead { get; set; } = new();
    [Required] public RequestRateDTO PushRegistration { get; set; } = new();
    [Required] public RequestRateDTO MediaToken { get; set; } = new();

    /// <summary>Every request of no other group.</summary>
    [Required] public RequestRateDTO Query { get; set; } = new();

    [Required] public RequestRateDTO Heartbeat { get; set; } = new();

    /// <summary>A connection that has this many requests refused within the window is closed.</summary>
    public int MaxRejectionsPerWindow { get; set; }
    public int RejectionWindowSeconds { get; set; }

    /// <summary>How many connections one IP address may keep open.</summary>
    public int MaxConnectionsPerIp { get; set; }
}

/// <summary>
/// A node of the SFU pool of the server that a coordinator can be given. Credentials are left out.
/// The connection fields are what the server knew when it answered; null for a disabled node.
/// </summary>
public class SfuNodeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public bool IsEnabled { get; set; }

    public SfuNodeConnectionState? ConnectionState { get; set; }

    /// <summary>Round trip of the server's last answered ping to the node; null while the node does not answer.</summary>
    public double? PingMs { get; set; }

    public DateTimeOffset? LastPingAtUtc { get; set; }

    /// <summary>Why the node is not connected, if known.</summary>
    public string? ConnectionError { get; set; }
}
