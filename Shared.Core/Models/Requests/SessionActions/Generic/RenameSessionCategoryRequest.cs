using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

public record RenameSessionCategoryRequest(
    [property: EntityId] int CategoryId,
    [property: Required, NotBlank, StringLength(Limits.SessionCategoryNameMaxLength), NoControlCharacters] string Name
) : GenericSessionRequest
{
    public SessionLayoutResponse ToResponse()
        => new(RequestId);
}
