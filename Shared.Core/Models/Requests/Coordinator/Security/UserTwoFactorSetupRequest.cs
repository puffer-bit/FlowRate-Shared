using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator.Security;

/// <summary>
/// Offers a new one-time code secret for the sender's authenticator app. It is used only once
/// <see cref="UserTwoFactorEnableRequest"/> sends a code made from it. Refused while codes are on.
/// </summary>
public record UserTwoFactorSetupRequest(UserSecurityProofDTO? Proof) : CoordinatorRequest
{
    public UserTwoFactorSetupResponse ToResponse(string secret, string uri)
        => new(RequestId, secret, uri);
}
