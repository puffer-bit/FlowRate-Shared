using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.Generic;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.Generic;

/// <summary>
/// Replaces the settings of a hybrid session with the ones given. The changed session reaches every
/// client, the sender too, through <see cref="Notifications.SessionInfo.HybridSessionReconfiguredNotification"/>.
/// </summary>
/// <param name="Capacity">Not below the number of peers in the session now.</param>
public record ChangeHybridSessionSettingsRequest(
    [property: EntityId] int SessionId,
    [property: Required, NotBlank, StringLength(Limits.SessionNameMaxLength), NoControlCharacters] string Name,
    [property: Range(Limits.SessionCapacityMin, Limits.SessionCapacityMax)] int Capacity,
    bool IsHostMustApprove,
    [property: Required] HybridMediaSettingsDTO Media
) : GenericSessionRequest
{
    public ChangeSessionSettingsResponse ToResponse()
        => new(RequestId, SessionId);
}
