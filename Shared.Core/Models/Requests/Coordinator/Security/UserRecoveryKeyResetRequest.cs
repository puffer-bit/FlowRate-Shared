using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator.Security;

namespace Shared.Models.Requests.Coordinator.Security;

/// <summary>
/// Replaces the recovery keys of the sender, made from a new recovery phrase; the old phrase stops working. The devices
/// of the account stay.
/// </summary>
public record UserRecoveryKeyResetRequest(
    UserSecurityProofDTO? Proof,
    [property: Required] RecoveryKeysDTO Keys) : CoordinatorRequest
{
    public UserRecoveryKeyResetResponse ToResponse() => new(RequestId);
}
