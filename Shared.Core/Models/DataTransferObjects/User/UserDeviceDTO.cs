using System.Text.Json.Serialization;

namespace Shared.Models.DataTransferObjects;

/// <summary>A device a user is connected from, as other users see it: no name, address or session.</summary>
/// <param name="Platform">One of <see cref="DevicePlatforms"/>.</param>
/// <param name="OsVersion">The system and its version; null when the client did not tell.</param>
/// <param name="Architecture">The processor architecture: "x64", "arm64"...; null when the client did not tell.</param>
/// <param name="AppVersion">The version of the app.</param>
public record UserDeviceDTO(string Platform, string? OsVersion, string? Architecture, string AppVersion)
{
    [JsonIgnore]
    public bool IsMobile => DevicePlatforms.IsMobile(Platform);
}

/// <summary>The platforms a client tells in <see cref="DeviceInfoDTO.Platform"/>.</summary>
public static class DevicePlatforms
{
    public const string Windows = "Windows";
    public const string Linux = "Linux";
    public const string MacOS = "macOS";
    public const string Android = "Android";
    public const string IOS = "iOS";

    /// <summary>A phone or a tablet: the user shows with a phone instead of the status dot.</summary>
    public static bool IsMobile(string? platform) =>
        string.Equals(platform, Android, StringComparison.OrdinalIgnoreCase)
        || string.Equals(platform, IOS, StringComparison.OrdinalIgnoreCase);
}
