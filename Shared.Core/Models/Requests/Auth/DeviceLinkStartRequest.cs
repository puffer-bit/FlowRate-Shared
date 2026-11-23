using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Auth;
using Shared.Validation;

namespace Shared.Models.Requests.Auth;

/// <summary>
/// A new device asks to be added to an account by one of its devices. The answer is a short code the new device shows,
/// as text and as a QR code; the connection then waits, logged out, for
/// <see cref="Notifications.CoordinatorInfo.DeviceLinkCompletedNotification"/> or
/// <see cref="Notifications.CoordinatorInfo.DeviceLinkDeclinedNotification"/>.
/// </summary>
public record DeviceLinkStartRequest(
    [property: StringLength(Limits.PasswordMaxLength)] string? ServerPassword,
    [property: Required] NewDeviceDTO NewDevice,
    DeviceInfoDTO? Device = null) : AuthRequest
{
    public DeviceLinkStartResponse ToResponse(string code, DateTimeOffset expiresAt) => new(RequestId, code, expiresAt);
}
