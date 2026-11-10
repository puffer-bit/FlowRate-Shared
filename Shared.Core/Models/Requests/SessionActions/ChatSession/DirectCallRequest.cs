using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.ChatSession;

public record DirectCallRequest([property: EntityId] int ChatId, Guid CallId,
    [property: DefinedEnum] DirectCallAction Action) : ChatSessionRequest, IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CallId == Guid.Empty) yield return new ValidationResult("Call ID must not be empty.", [nameof(CallId)]);
    }

    public DirectCallResponse ToResponse(DirectCallDTO call) => new(RequestId, call);
}
