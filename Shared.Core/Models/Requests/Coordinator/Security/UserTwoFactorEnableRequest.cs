using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator.Security;

/// <summary>Turns one-time codes on with a code made from the offered secret.</summary>
public record UserTwoFactorEnableRequest(
    [property: Required, StringLength(Limits.TwoFactorMaxLength), NoControlCharacters] string Code) : CoordinatorRequest
{
    public UserSecurityResponse ToResponse(UserSecurityDTO security)
        => new(RequestId, security);
}
