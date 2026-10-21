using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Finds users whose name starts with or contains <see cref="Query"/>, those starting with it first.
/// An empty query returns the users online first, then the others.
/// </summary>
public record CoordinatorUserSearchRequest(
    [property: StringLength(Limits.UsernameMaxLength), NoControlCharacters] string Query,
    [property: Range(1, Limits.UserSearchMaxCount)] int Limit) : CoordinatorRequest
{
    public CoordinatorUserSearchResponse ToResponse(List<UserDTO> users, UsersRequestResult result)
        => new(RequestId, users, result);
}
