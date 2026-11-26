using Shared.Enums.User;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Tells how long the sending device has gone without input. Not saved: the level of a device ends with its
/// connection. Others see <see cref="UserStatus.Idle"/> or <see cref="UserStatus.Sleeping"/> instead of
/// <see cref="UserStatus.Online"/> while every connected device of the user is at least that idle.
/// </summary>
public record CoordinatorSetIdleRequest(
    [property: DefinedEnum] IdleLevel Level) : CoordinatorRequest
{
    public CoordinatorSetIdleResponse ToResponse()
        => new(RequestId, Level);
}
