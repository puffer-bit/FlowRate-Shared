namespace Shared.Models.Responses.SessionActions.Generic;

/// <summary>
/// The answer to a change of the session layout. The new layout itself reaches every client,
/// the sender too, through <see cref="Notifications.SessionInfo.SessionLayoutChangedNotification"/>.
/// </summary>
/// <param name="CategoryId">The category a create request added.</param>
public record SessionLayoutResponse(
    Guid RequestId,
    int? CategoryId = null) : GenericSessionResponse(RequestId);
