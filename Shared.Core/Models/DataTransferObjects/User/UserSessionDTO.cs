using System.ComponentModel.DataAnnotations;
using Shared.Validation;

namespace Shared.Models.DataTransferObjects;

/// <summary>What the client tells about the device it logs in from, shown in the list of sessions.</summary>
/// <param name="Name">The machine name, "Pixel 8"...</param>
/// <param name="Platform">"Linux", "Windows", "Android"...</param>
public record DeviceInfoDTO(
    [property: StringLength(Limits.DeviceNameMaxLength), NoControlCharacters] string Name,
    [property: StringLength(Limits.DevicePlatformMaxLength), NoControlCharacters] string Platform,
    [property: StringLength(Limits.AppVersionMaxLength), NoControlCharacters] string AppVersion);

/// <summary>A device the user is logged in from.</summary>
/// <param name="LastUsedAtUtc">The last login of the device; the session ends after a while without one.</param>
/// <param name="LastAddress">The IP address of the last login.</param>
/// <param name="IsCurrent">The session of the connection that asked.</param>
/// <param name="IsOnline">The device is connected now.</param>
public record UserSessionDTO(
    Guid Id,
    DeviceInfoDTO Device,
    DateTime CreatedAtUtc,
    DateTime LastUsedAtUtc,
    DateTime ExpiresAtUtc,
    string? LastAddress,
    bool IsCurrent,
    bool IsOnline);
