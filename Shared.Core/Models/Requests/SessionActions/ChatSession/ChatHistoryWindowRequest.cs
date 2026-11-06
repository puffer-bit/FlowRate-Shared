using System.ComponentModel.DataAnnotations;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>Reads a bounded history window using an exclusive cursor, or includes it for Around.</summary>
public record ChatHistoryWindowRequest(
    [property: EntityId] int ChatId,
    [property: EntityId] int MessageId,
    [property: EnumDataType(typeof(ChatHistoryDirection))] ChatHistoryDirection Direction,
    [property: Range(1, Limits.ChatHistoryMaxTake)] int Take
) : ChatSessionRequest;

public enum ChatHistoryDirection
{
    Before,
    After,
    Around
}
