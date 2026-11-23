using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator.Devices;

/// <param name="Devices">The devices of the account, the latest used first.</param>
public record AccountDevicesResponse(
    Guid RequestId,
    List<AccountDeviceDTO> Devices) : CoordinatorResponse(RequestId);

public record LogoutResponse(Guid RequestId) : CoordinatorResponse(RequestId);

/// <param name="Device">What the new device told about itself; null when it did not.</param>
/// <param name="Address">The IP address it connected from.</param>
public record DeviceLinkInfoResponse(
    Guid RequestId,
    DeviceInfoDTO? Device,
    string? Address) : CoordinatorResponse(RequestId);

public record DeviceLinkApproveResponse(Guid RequestId) : CoordinatorResponse(RequestId);
