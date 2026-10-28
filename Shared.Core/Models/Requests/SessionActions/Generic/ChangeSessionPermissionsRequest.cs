using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

/// <summary>
/// Replaces the permission overrides of a session, at most one per role. Needs
/// <see cref="Permission.ManageSessions"/> and <see cref="Permission.ManageRoles"/>; the overrides of roles
/// not below one's own highest role must stay as they are, and only permissions one has can be allowed or
/// denied. The changed session reaches every client that may still see it through the reconfigured
/// notification of its type; a client that no longer may, or now may, gets the deleted or created one.
/// </summary>
public record ChangeSessionPermissionsRequest(
    [property: EntityId] int SessionId,
    [property: Required, ItemCount(0, Limits.RolesMaxCount)] List<SessionPermissionOverrideDTO> Overrides
) : GenericSessionRequest
{
    public ChangeSessionPermissionsResponse ToResponse()
        => new(RequestId, SessionId);
}
