namespace Shared.Models.DataTransferObjects;

/// <summary>
/// Start opens the call and joins it; Join enters a running call (answering a ring is a join); Decline turns down
/// one's own ring; Leave hangs up; Ring calls a member again (TargetUserId) or every member not in the call.
/// </summary>
public enum ChatCallAction { Start = 0, Join = 1, Decline = 2, Leave = 3, Ring = 4 }

public enum ChatCallState { Active = 0, Ended = 1 }

/// <summary>Where a member stands in a call. Members that were never rung and never joined are not listed.</summary>
public enum ChatCallParticipantState { Ringing = 0, Joined = 1, Declined = 2, Missed = 3, Left = 4 }

/// <summary>Stored in call messages; values stay fixed.</summary>
public enum ChatCallEndReason { Completed = 0, Declined = 1, Cancelled = 2, Missed = 3, Disconnected = 4 }

public sealed record ChatCallParticipantDTO(int UserId, ChatCallParticipantState State);

/// <summary>
/// A call in a direct chat or a group. It lives while anyone is in it. StartedAt is when a second member first
/// joined. IsSelectedDevice tells whether this connection is the one taking part; only that connection gets
/// Session. Device selection is never sent as a connection identifier.
/// </summary>
public sealed record ChatCallDTO(Guid CallId, int ChatId, int CallerId, ChatCallState State,
    ChatCallEndReason? EndReason, DateTimeOffset CreatedAt, DateTimeOffset? StartedAt,
    IReadOnlyList<ChatCallParticipantDTO> Participants, bool IsSelectedDevice, HybridSessionDTO? Session);
