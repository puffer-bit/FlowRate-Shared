using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

/// <param name="Index">The place among the categories once this one is taken out; past the end means the end.</param>
public record MoveSessionCategoryRequest(
    [property: EntityId] int CategoryId,
    [property: Range(0, Limits.SessionLayoutIndexMax)] int Index
) : GenericSessionRequest
{
    public SessionLayoutResponse ToResponse(SessionLayoutResult result)
        => new(RequestId, result);
}
