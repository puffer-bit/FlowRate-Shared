using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator.Security;

/// <summary>
/// What the sender logs in with. The requests of this folder are all about the sender's own account; a change asks
/// for proof while the user has a password or one-time codes (see <see cref="UserSecurityProofDTO"/>).
/// </summary>
public record UserSecurityInfoRequest : CoordinatorRequest
{
    public UserSecurityResponse ToResponse(UserSecurityDTO security)
        => new(RequestId, security);
}
