using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>Reads all pins independently of the currently loaded history.</summary>
public record ChatPinnedMessagesRequest([property: EntityId] int ChatId) : ChatSessionRequest;
