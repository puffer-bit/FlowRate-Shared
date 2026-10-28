using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Deletes another user with their roles; their messages stay. A connected user is disconnected, and connecting
/// again makes them a new user.
/// </summary>
public record CoordinatorDeleteUserRequest([property: EntityId] int TargetUserId) : CoordinatorRequest
{
    public CoordinatorDeleteUserResponse ToResponse()
        => new(RequestId, TargetUserId);
}
