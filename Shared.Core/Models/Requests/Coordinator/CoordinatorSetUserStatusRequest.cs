using Shared.Enums.User;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Sets the status of the sender; kept for the next logins. <see cref="UserStatus.Offline"/> is refused.
/// </summary>
public record CoordinatorSetUserStatusRequest(
    [property: DefinedEnum] UserStatus Status) : CoordinatorRequest
{
    public CoordinatorSetUserStatusResponse ToResponse(SetUserStatusResult result, UserDTO? user = null)
        => new(RequestId, result, user);
}
