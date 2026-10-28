using Shared.Models.Responses.Coordinator;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Asks for the token that authorizes the sender on the media endpoints of this coordinator
/// (see <see cref="Shared.Models.Media.MediaEndpoints"/>). The token is short-lived; a new one is
/// requested when it expires or an endpoint answers 401.
/// </summary>
public record MediaTokenRequest : CoordinatorRequest
{
    public MediaTokenResponse ToResponse(string token, DateTime expiresAt)
        => new(RequestId, token, expiresAt);
}
