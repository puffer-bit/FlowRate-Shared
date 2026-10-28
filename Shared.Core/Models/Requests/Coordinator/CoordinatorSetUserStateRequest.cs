using Shared.Enums.User;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Bans, freezes or reinstates another user: <see cref="UserState.Banned"/>, <see cref="UserState.Suspended"/> or
/// <see cref="UserState.Normal"/>. A banned user is disconnected and cannot log in; a frozen one can log in and read,
/// but not write, join voice or change anything.
/// </summary>
public record CoordinatorSetUserStateRequest(
    [property: EntityId] int UserTargetId,
    [property: DefinedEnum] UserState State) : CoordinatorRequest
{
    public CoordinatorSetUserStateResponse ToResponse(UserDTO user)
        => new(RequestId, UserTargetId, user);
}
