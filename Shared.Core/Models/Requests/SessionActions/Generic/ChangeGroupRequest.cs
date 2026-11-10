using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

/// <summary>Replaces a group's name, members and owner. A member may instead leave their group.</summary>
public record ChangeGroupRequest(
    [property: EntityId] int SessionId,
    [property: Required, NotBlank, StringLength(Limits.SessionNameMaxLength), NoControlCharacters] string Name,
    [property: Required, ItemCount(0, 100)] int[] MemberIds,
    [property: EntityId] int OwnerId,
    bool Leave = false
) : GenericSessionRequest, IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MemberIds.Length > 100 || MemberIds.Any(id => id <= 0))
            yield return new ValidationResult("Invalid group membership.", [nameof(MemberIds)]);
    }

    public ChangeGroupResponse ToResponse() => new(RequestId, SessionId);
}
