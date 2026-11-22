using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>TargetUserId is only for Ring; without it Ring calls every member not in the call.</summary>
public record ChatCallRequest([property: EntityId] int ChatId, [property: NotEmptyGuid] Guid CallId,
    [property: DefinedEnum] ChatCallAction Action, [property: EntityId] int? TargetUserId = null) : ChatSessionRequest
{
    public ChatCallResponse ToResponse(ChatCallDTO call) => new(RequestId, call);
}
