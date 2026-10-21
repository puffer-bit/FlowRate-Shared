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

    public IceServerSettingsDTO Stun { get; set; } = new();
    public IceServerSettingsDTO Turn { get; set; } = new();

    public long MaxFileSizeBytes { get; set; }

    /// <summary>0 when the media storage is not limited.</summary>
    public long StorageQuotaBytes { get; set; }

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

    [Required]
    public IceServerSettingsDTO Stun { get; set; } = new();

    [Required]
    public IceServerSettingsDTO Turn { get; set; } = new();

    public long MaxFileSizeBytes { get; set; }
    public long StorageQuotaBytes { get; set; }

    [Required, ItemCount(Limits.SfuNodesMaxCount)]
    public List<int> SfuNodeIds { get; set; } = [];
}

/// <summary>A STUN or a TURN server; STUN has no credentials.</summary>
public class IceServerSettingsDTO
{
    public bool IsEnabled { get; set; }

    [StringLength(Limits.IpAddressMaxLength)]
    public string? Address { get; set; }

    public int? Port { get; set; }

    [StringLength(Limits.IceCredentialMaxLength)]
    public string? Username { get; set; }

    [StringLength(Limits.IceCredentialMaxLength)]
    public string? Password { get; set; }
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
