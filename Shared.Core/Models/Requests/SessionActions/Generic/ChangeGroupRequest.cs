using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

/// <summary>Replaces a group's name, members and owner.</summary>
public record ChangeGroupRequest(
    [property: EntityId] int SessionId,
    [property: Required, NotBlank, StringLength(Limits.SessionNameMaxLength), NoControlCharacters] string Name,
    [property: Required, ItemCount(0, Limits.GroupMembersMaxCount)] int[] MemberIds,
    [property: EntityId] int OwnerUserId
) : GenericSessionRequest, IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MemberIds.Any(id => id <= 0))
            yield return new ValidationResult("Member ids must be positive.", [nameof(MemberIds)]);
        if (MemberIds.Distinct().Count() != MemberIds.Length)
            yield return new ValidationResult("Member ids must not repeat.", [nameof(MemberIds)]);
    }

    public ChangeGroupResponse ToResponse() => new(RequestId, SessionId);
}

/// <summary>The sender leaves a group they are a member of. The owner cannot (<c>Conflict</c>): they hand the group to another member first.</summary>
public record LeaveGroupRequest([property: EntityId] int SessionId) : GenericSessionRequest
{
    public LeaveGroupResponse ToResponse() => new(RequestId, SessionId);
}
