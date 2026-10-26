using System.ComponentModel.DataAnnotations;
using Shared.Enums.User;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator.Security;

/// <summary>Options to make a new passkey with; the answer goes to <see cref="UserPasskeyRegisterRequest"/>.</summary>
public record UserPasskeyOptionsRequest(UserSecurityProofDTO? Proof) : CoordinatorRequest
{
    public UserPasskeyOptionsResponse ToResponse(UserSecurityResult result, string? optionsJson = null)
        => new(RequestId, result, optionsJson);
}
