using System.ComponentModel.DataAnnotations;
using Shared.Enums.User;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator.Security;

/// <summary>Sets a new password (8 to 128 characters), or removes it when <paramref name="NewPassword"/> is null.</summary>
public record UserSetPasswordRequest(
    UserSecurityProofDTO? Proof,
    [property: StringLength(Limits.PasswordMaxLength)] string? NewPassword) : CoordinatorRequest
{
    public UserSecurityResponse ToResponse(UserSecurityResult result, UserSecurityDTO? security = null)
        => new(RequestId, result, security);
}
