using Shared.Models.DataTransferObjects;
namespace Shared.Models.Notifications.SessionInfo;
public record DirectCallChangedNotification(DirectCallDTO Call) : Notification;
