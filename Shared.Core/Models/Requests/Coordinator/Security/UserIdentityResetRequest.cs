using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;

namespace Shared.Models.Requests.Coordinator.Security;

/// <summary>
/// Gives the sender a new identity. The old one stops working and every session but the current one ends, so a
/// leaked identity is of no use any more.
/// </summary>
public record UserIdentityResetRequest(UserSecurityProofDTO? Proof) : CoordinatorRequest
{
    public UserIdentityResetResponse ToResponse(string userIdentity)
        => new(RequestId, userIdentity);
}
