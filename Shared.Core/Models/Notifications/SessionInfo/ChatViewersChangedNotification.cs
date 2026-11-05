using Shared.Models.DataTransferObjects;

namespace Shared.Models.Notifications.SessionInfo;

/// <summary>Only the changed audience rows, scoped to one device subscription.</summary>
public record ChatViewersChangedNotification(int ChatId, Guid SubscriptionId,
    List<UserDTO> Upserted, List<int> Removed) : BaseMessage;
