using System.ComponentModel.DataAnnotations;
using Shared.Models.Requests.SessionActions.Generic.Models.SessionData;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic.Models;

/// <summary>The host of the new session is always the user who sends the request.</summary>
public record CreateSessionModel(
    [property: Required, NotBlank, StringLength(Limits.SessionNameMaxLength), NoControlCharacters] string Name,
    [property: Range(Limits.SessionCapacityMin, Limits.SessionCapacityMax)] int Capacity,
    [property: Required] CreateSessionData SessionData,
    [property: EntityId] int? CategoryId = null
);
