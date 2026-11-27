using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator.Security;

public record UserPasskeyDeleteRequest(
    UserSecurityProofDTO? Proof,
    [property: EntityId] int PasskeyId) : CoordinatorRequest
{
    public UserSecurityResponse ToResponse(UserSecurityDTO security)
        => new(RequestId, security);
}
