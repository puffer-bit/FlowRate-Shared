using Shared.Enums.User;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;

namespace Shared.Models.Requests.Coordinator.Security;

/// <summary>
/// Ends a session of the sender, or every session but the current one when <paramref name="SessionId"/> is null.
/// The devices of those sessions are disconnected and have to log in with the identity again. Ending a session takes
/// nothing from the account, so no proof is asked.
/// </summary>
public record UserSessionRevokeRequest(Guid? SessionId) : CoordinatorRequest
{
    public UserSessionsResponse ToResponse(UserSecurityResult result, List<UserSessionDTO>? sessions = null)
        => new(RequestId, result, sessions);
}
