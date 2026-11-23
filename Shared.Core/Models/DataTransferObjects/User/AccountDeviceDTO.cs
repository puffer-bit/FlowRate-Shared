using System.ComponentModel.DataAnnotations;
using Shared.Enums.Auth;
using Shared.Validation;

namespace Shared.Models.DataTransferObjects;

/// <summary>What the client tells about the device it logs in from, shown in the list of devices.</summary>
/// <param name="Name">The machine name, "Pixel 8"...</param>
/// <param name="Platform">One of <see cref="DevicePlatforms"/>: "Linux", "Windows", "Android"...</param>
/// <param name="OsVersion">The system and its version: "Windows 11 (26100)", "Android 16", "Ubuntu 26.04 LTS"...</param>
/// <param name="Architecture">The processor architecture the app runs on: "x64", "arm64"...</param>
public record DeviceInfoDTO(
    [property: StringLength(Limits.DeviceNameMaxLength), NoControlCharacters] string Name,
    [property: StringLength(Limits.DevicePlatformMaxLength), NoControlCharacters] string Platform,
    [property: StringLength(Limits.AppVersionMaxLength), NoControlCharacters] string AppVersion,
    [property: StringLength(Limits.OsVersionMaxLength), NoControlCharacters] string? OsVersion = null,
    [property: StringLength(Limits.ArchitectureMaxLength), NoControlCharacters] string? Architecture = null);

/// <summary>A device of the user's account.</summary>
/// <param name="LastUsedAt">The last login of the device; it ends after a while without one.</param>
/// <param name="ExpiresAt">When the device ends unless it logs in again.</param>
/// <param name="LastAddress">The IP address of the last login.</param>
/// <param name="IsCurrent">The device of the connection that asked.</param>
/// <param name="IsOnline">The device is connected now.</param>
/// <param name="IsTemporary">Not remembered: it ends after a day without a login.</param>
/// <param name="AddedBy">How the device was added to the account.</param>
public record AccountDeviceDTO(
    Guid Id,
    DeviceInfoDTO Device,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastUsedAt,
    DateTimeOffset ExpiresAt,
    string? LastAddress,
    bool IsCurrent,
    bool IsOnline,
    bool IsTemporary,
    DeviceLoginMethod AddedBy);
