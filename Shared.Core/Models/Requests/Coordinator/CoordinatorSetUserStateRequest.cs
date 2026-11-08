using System.ComponentModel.DataAnnotations;
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
    [property: EntityId] int TargetUserId,
    [property: DefinedEnum] UserState State,
    [property: DefinedEnum] BanKind Kind = BanKind.Account,
    [property: StringLength(1000)] string? Reason = null,
    DateTime? ExpiresAt = null,
    [property: StringLength(45)] string? Address = null) : CoordinatorRequest
{
    public CoordinatorSetUserStateResponse ToResponse(UserDTO user)
        => new(RequestId, TargetUserId, user);
}
