using System.ComponentModel.DataAnnotations;
using Shared.Models.Requests.SessionActions.Generic.Models.SessionData;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic.Models;

/// <summary>The host of the new session is always the user who sends the request.</summary>
public record CreateSessionModel(
    [property: Required, NotBlank, StringLength(Limits.SessionNameMaxLength), NoControlCharacters] string Name,
    int Capacity,
    [property: Required] CreateSessionData SessionData,
    [property: EntityId] int? CategoryId = null
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Chat capacity is ignored; positive values from older clients remain accepted.
        if (SessionData is not ChatSessionCreateData &&
            Capacity is < Limits.SessionCapacityMin or > Limits.SessionCapacityMax)
            yield return new ValidationResult(
                $"Capacity must be between {Limits.SessionCapacityMin} and {Limits.SessionCapacityMax}.",
                [nameof(Capacity)]);
    }
}
