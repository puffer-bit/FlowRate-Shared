using System.ComponentModel.DataAnnotations;
using Shared.Enums.User;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Renames a connected user, the sender when <see cref="UserId"/> is null; the name must not be taken by another user.
/// </summary>
public record CoordinatorChangeUsernameRequest(
    [property: Required, NotBlank, StringLength(Limits.UsernameMaxLength), NoControlCharacters] string Username,
    int? UserId = null) : CoordinatorRequest
{
    public CoordinatorChangeUsernameResponse ToResponse(ChangeUsernameResult result, UserDTO? user = null)
        => new(RequestId, result, user);
}
