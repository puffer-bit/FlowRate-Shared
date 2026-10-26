using System.ComponentModel.DataAnnotations;
using Shared.Enums.User;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator.Security;

public record UserTwoFactorDisableRequest(UserSecurityProofDTO? Proof) : CoordinatorRequest
{
    public UserSecurityResponse ToResponse(UserSecurityResult result, UserSecurityDTO? security = null)
        => new(RequestId, result, security);
}
