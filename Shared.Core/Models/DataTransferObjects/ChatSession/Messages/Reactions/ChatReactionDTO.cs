namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Reactions;

public class ChatReactionDTO
{
    public int UserId { get; set; }
    public required string Emoji { get; init; }

    /// <summary>When the reaction was set (UTC); null for reactions set before the time was kept.</summary>
    public DateTime? ReactedAt { get; init; }
}
