using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Devices;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator.Devices;

/// <summary>The devices of the sender's account.</summary>
public record AccountDevicesRequest : CoordinatorRequest
{
    public AccountDevicesResponse ToResponse(List<AccountDeviceDTO> devices) => new(RequestId, devices);
}

/// <summary>
/// Removes a device of the sender's account, or every device but the current one when <paramref name="DeviceId"/> is
/// null. Connected ones are disconnected; each has to be added again. Removing a device takes nothing from the account,
/// so no proof is asked. The current device leaves with <see cref="LogoutRequest"/>.
/// </summary>
public record AccountDeviceRevokeRequest(Guid? DeviceId) : CoordinatorRequest
{
    public AccountDevicesResponse ToResponse(List<AccountDeviceDTO> devices) => new(RequestId, devices);
}

/// <summary>Removes the current device from the account; the server answers and closes the connection.</summary>
public record LogoutRequest : CoordinatorRequest
{
    public LogoutResponse ToResponse() => new(RequestId);
}

/// <summary>What the new device that showed <paramref name="Code"/> tells about itself, before it is approved.</summary>
public record DeviceLinkInfoRequest(
    [property: Required, StringLength(Limits.LinkCodeMaxLength), NoControlCharacters] string Code) : CoordinatorRequest
{
    public DeviceLinkInfoResponse ToResponse(DeviceInfoDTO? device, string? address) => new(RequestId, device, address);
}

/// <summary>
/// Adds the new device that showed <paramref name="Code"/> to the sender's account, or turns it away. The new device is
/// logged in at once.
/// </summary>
public record DeviceLinkApproveRequest(
    [property: Required, StringLength(Limits.LinkCodeMaxLength), NoControlCharacters] string Code,
    bool Approve) : CoordinatorRequest
{
    public DeviceLinkApproveResponse ToResponse() => new(RequestId);
}
