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

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset FirstLaunchedAt { get; set; }
    public DateTimeOffset LaunchedAt { get; set; }
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

    /// <summary>The longest side images are reduced to when compressed before sending.</summary>
    public int ImageMaxSide { get; set; }

    /// <summary>The JPEG or WebP quality of compressed images, from 30 to 100.</summary>
    public int ImageQuality { get; set; }

    /// <summary>
    /// Images may be sent as they are. Otherwise the server refuses any larger than <see cref="ImageMaxSide"/>.
    /// </summary>
    public bool AllowOriginalImages { get; set; }

    /// <summary>
    /// The shorter side videos are reduced to when compressed before sending, like 720 for 720p.
    /// </summary>
    public int VideoMaxSide { get; set; }

    /// <summary>
    /// Videos may be sent as they are. Otherwise the server refuses one that is not an MP4 within
    /// <see cref="VideoMaxSide"/> and its bitrate.
    /// </summary>
    public bool AllowOriginalVideos { get; set; }

    /// <summary>The revision of the stored settings these values come from.</summary>
    public long SettingsRevision { get; set; }
}
