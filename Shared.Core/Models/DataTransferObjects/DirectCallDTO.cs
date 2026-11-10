namespace Shared.Models.DataTransferObjects;

public enum DirectCallAction { Start = 0, Accept = 1, Decline = 2, End = 3 }
public enum DirectCallState { Ringing = 0, Active = 1, Ended = 2 }
public enum DirectCallEndReason { Completed = 0, Declined = 1, Cancelled = 2, Missed = 3, Disconnected = 4 }

/// <summary>A transient call. Device selection is sent individually, never as a connection identifier.</summary>
public sealed record DirectCallDTO(Guid CallId, int ChatId, int CallerId, int CalleeId,
    DirectCallState State, DirectCallEndReason? EndReason, DateTimeOffset CreatedAt,
    DateTimeOffset? StartedAt, bool IsSelectedDevice, HybridSessionDTO? Session);
