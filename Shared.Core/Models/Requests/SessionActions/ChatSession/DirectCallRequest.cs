using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

public record DirectCallRequest([property: EntityId] int ChatId, [property: NotEmptyGuid] Guid CallId,
    [property: DefinedEnum] DirectCallAction Action) : ChatSessionRequest
{
    public DirectCallResponse ToResponse(DirectCallDTO call) => new(RequestId, call);
}
