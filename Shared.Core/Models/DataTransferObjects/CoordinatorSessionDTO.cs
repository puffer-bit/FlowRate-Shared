namespace Shared.Models.DataTransferObjects;

public class CoordinatorSessionDTO
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Port { get; set; }

    public int ConnectedUsersCount { get; set; }

    public string ServerVersion { get; set; } = string.Empty;

    /// <summary>The OS and architecture the server runs on, such as "Linux x64".</summary>
    public string ServerPlatform { get; set; } = string.Empty;

    public DateTime CreateTime { get; set; }
    public DateTime FirstLaunchTime { get; set; }
    public DateTime CurrentLaunchTime { get; set; }
    public int MaxOnlineUsers { get; set; }
    public int MaxUsers { get; set; }

    public bool IsMOTDEnabled { get; set; }
    public string? MOTDText { get; set; }

    /// <summary>Users must enter the server password to sign in.</summary>
    public bool IsPasswordProtected { get; set; }

    public int HeartbeatIntervalSeconds { get; set; }
    public int HeartbeatTimeoutSeconds { get; set; }

    public long MaxFileSizeBytes { get; set; }

    /// <summary>0 when the media storage is not limited.</summary>
    public long StorageQuotaBytes { get; set; }

    /// <summary>The revision of the stored settings these values come from.</summary>
    public long SettingsRevision { get; set; }
}
