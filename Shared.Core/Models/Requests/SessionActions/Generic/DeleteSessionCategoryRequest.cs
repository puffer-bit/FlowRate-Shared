using Shared.Enums;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

/// <summary>Deletes a category; its sessions go to the end of the sessions outside any category.</summary>
public record DeleteSessionCategoryRequest(
    [property: EntityId] int CategoryId
) : GenericSessionRequest
{
    public SessionLayoutResponse ToResponse(SessionLayoutResult result)
        => new(RequestId, result);
}
