using System.ComponentModel.DataAnnotations;
using Shared.Enums.User;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Freezes another user (<see cref="UserState.Suspended"/>) or reinstates them (<see cref="UserState.Normal"/>):
/// a frozen user can log in and read, but not write, join voice or change anything. Bans have their own
/// requests, <see cref="CoordinatorBanRequest"/> and <see cref="CoordinatorRevokeBanRequest"/>.
/// </summary>
public record CoordinatorSetUserStateRequest(
    [property: EntityId] int TargetUserId,
    [property: DefinedEnum] UserState State) : CoordinatorRequest, IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (State is not (UserState.Normal or UserState.Suspended))
            yield return new ValidationResult("The state must be Normal or Suspended.", [nameof(State)]);
    }

    public CoordinatorSetUserStateResponse ToResponse(UserDTO user)
        => new(RequestId, TargetUserId, user);
}
