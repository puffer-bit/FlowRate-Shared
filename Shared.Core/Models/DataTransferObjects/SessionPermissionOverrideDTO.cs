using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Validation;

namespace Shared.Models.DataTransferObjects;

/// <summary>
/// Changes, in one session, what a role allows. A flag in neither <see cref="Allow"/> nor <see cref="Deny"/>
/// stays as the role has it on the coordinator.
/// </summary>
public class SessionPermissionOverrideDTO : IValidatableObject
{
    /// <summary>The role, the everyone role included.</summary>
    [EntityId]
    public int RoleId { get; set; }

    public Permission Allow { get; set; }
    public Permission Deny { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if ((Allow & Deny) != 0)
            yield return new ValidationResult("A permission is both allowed and denied.", [nameof(Allow), nameof(Deny)]);
        if (((Allow | Deny) & ~Permission.SessionScoped) != 0)
            yield return new ValidationResult("Only session permissions can be overridden.", [nameof(Allow), nameof(Deny)]);
    }
}
