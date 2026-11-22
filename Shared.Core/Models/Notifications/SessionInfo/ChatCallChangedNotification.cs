using Shared.Models.DataTransferObjects;
namespace Shared.Models.Notifications.SessionInfo;
/// <summary>Sent to every member of the chat, so they can see a call is running and join it.</summary>
public record ChatCallChangedNotification(ChatCallDTO Call) : Notification;
