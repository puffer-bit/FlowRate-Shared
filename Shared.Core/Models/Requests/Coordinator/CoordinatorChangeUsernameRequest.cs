using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Renames a connected user, the sender when <see cref="TargetUserId"/> is null; the name must not be taken by another user.
/// </summary>
public record CoordinatorChangeUsernameRequest(
    [property: Required, NotBlank, StringLength(Limits.UsernameMaxLength), NoControlCharacters] string Username,
    int? TargetUserId = null) : CoordinatorRequest
{
    public CoordinatorChangeUsernameResponse ToResponse(UserDTO user)
        => new(RequestId, user);
}
