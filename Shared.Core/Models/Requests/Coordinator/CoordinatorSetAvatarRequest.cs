using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Puts an uploaded picture on the profile of the sender, or takes the avatar off a connected user
/// when <see cref="UploadId"/> is null. A picture can only be set for the sender themselves.
/// </summary>
/// <param name="UploadId">
/// A finished upload of the kind <see cref="Shared.Models.Media.MediaUploadMetadata.KindAvatar"/>.
/// </param>
public record CoordinatorSetAvatarRequest(
    [property: StringLength(Limits.MediaUploadIdLength * 2), HexBytes(Limits.MediaUploadIdLength)] string? UploadId,
    int? TargetUserId = null) : CoordinatorRequest
{
    public CoordinatorSetAvatarResponse ToResponse(UserDTO user)
        => new(RequestId, user);
}
