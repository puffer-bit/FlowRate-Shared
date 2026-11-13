namespace Shared.Models.DataTransferObjects.ChatSession;

/// <summary>
/// A member who has read a message, with the time (UTC) their read position first reached it; null when
/// the position got there before read times were kept.
/// </summary>
public record ChatMessageReaderDTO(int UserId, DateTime? ReadAt);
