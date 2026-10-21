using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

/// <summary>Adds a category at the end of the list.</summary>
public record CreateSessionCategoryRequest(
    [property: Required, NotBlank, StringLength(Limits.SessionCategoryNameMaxLength), NoControlCharacters] string Name
) : GenericSessionRequest
{
    public SessionLayoutResponse ToResponse(SessionLayoutResult result, int? categoryId = null)
        => new(RequestId, result, categoryId);
}
