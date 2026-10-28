using Shared.Enums.User;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;

namespace Shared.Models.Requests.Coordinator.Security;

/// <summary>The devices the sender is logged in from.</summary>
public record UserSessionsRequest : CoordinatorRequest
{
    public UserSessionsResponse ToResponse(UserSecurityResult result, List<UserSessionDTO>? sessions = null)
        => new(RequestId, result, sessions);
}
