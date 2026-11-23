using Shared.Models.Responses.Auth;

namespace Shared.Models.Requests.Auth;

/// <summary>
/// Asks for the challenge a device or the recovery key signs in its <see cref="LoginRequest"/>. Each one is good for
/// one login of this connection and a short time.
/// </summary>
public record LoginChallengeRequest : AuthRequest
{
    public LoginChallengeResponse ToResponse(string nonce, string coordinatorId) => new(RequestId, nonce, coordinatorId);
}
