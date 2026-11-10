namespace Shared.Models.DataTransferObjects.ChatSession.Messages;

public enum ChatSystemEventKind
{
    MemberAdded = 1,
    MemberRemoved = 2,
    MemberLeft = 3,
    CallStarted = 4,
    CallEnded = 5
}

/// <summary>Server-owned event data. TextContent supplies a fallback for older clients.</summary>
public sealed record ChatSystemContent
{
    public required ChatSystemEventKind Kind { get; init; }
    public int? ActorUserId { get; init; }
    public int? SubjectUserId { get; init; }
    public Guid? CallId { get; init; }

    public bool IsValid() => Enum.IsDefined(Kind) && ActorUserId is not <= 0 && (Kind switch
    {
        ChatSystemEventKind.MemberAdded or ChatSystemEventKind.MemberRemoved =>
            ActorUserId.HasValue && SubjectUserId is > 0 && CallId == null,
        ChatSystemEventKind.MemberLeft =>
            ActorUserId is > 0 && SubjectUserId == ActorUserId && CallId == null,
        ChatSystemEventKind.CallStarted =>
            ActorUserId.HasValue && SubjectUserId == null && CallId.HasValue && CallId != Guid.Empty,
        ChatSystemEventKind.CallEnded =>
            SubjectUserId == null && CallId.HasValue && CallId != Guid.Empty,
        _ => false
    });

    public string ToFallbackText() => Kind switch
    {
        ChatSystemEventKind.MemberAdded => $"User {ActorUserId} added user {SubjectUserId}.",
        ChatSystemEventKind.MemberRemoved => $"User {ActorUserId} removed user {SubjectUserId}.",
        ChatSystemEventKind.MemberLeft => $"User {SubjectUserId} left the group.",
        ChatSystemEventKind.CallStarted => $"User {ActorUserId} started a call.",
        ChatSystemEventKind.CallEnded => "The call ended.",
        _ => throw new InvalidOperationException("Unknown system event.")
    };
}
