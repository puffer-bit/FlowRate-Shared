using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

/// <param name="CategoryId">The category to put the session in, or null for the sessions outside any category.</param>
/// <param name="Index">The place among the sessions there once the session is taken out; past the end means the end.</param>
public record MoveSessionRequest(
    [property: EntityId] int SessionId,
    [property: EntityId] int? CategoryId,
    [property: Range(0, Limits.SessionLayoutIndexMax)] int Index
) : GenericSessionRequest
{
    public SessionLayoutResponse ToResponse()
        => new(RequestId);
}
