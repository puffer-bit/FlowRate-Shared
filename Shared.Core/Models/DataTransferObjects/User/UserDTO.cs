using Shared.Enums.User;

namespace Shared.Models.DataTransferObjects;

public class UserDTO
{
    public int Id { get; set; }
    public string CoordinatorInstanceId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? Ip { get; set; }
    public string? RegionName { get; set; }
    public string? RegionCode { get; set; }
    public string? DisplayName { get; set; }
    public string? Prefix { get; set; }
    public string? AvatarUrl { get; set; }
    public UserState State { get; set; }

    /// <summary>
    /// The status as the receiver sees it: <see cref="UserStatus.Offline"/> for a user who is not connected or is
    /// invisible to others. A user always gets the status they picked for themselves.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// The device the user is connected from: a phone when one is connected, otherwise the device connected last.
    /// Null while others see the user offline; a user always gets their own.
    /// </summary>
    public UserDeviceDTO? Device { get; set; }

    public DateTimeOffset FirstConnectedAt { get; set; }
    public DateTimeOffset LastConnectedAt { get; set; }

    /// <summary>The coordinator roles of the user, without the everyone role.</summary>
    public List<int> RoleIds { get; set; } = [];
}
