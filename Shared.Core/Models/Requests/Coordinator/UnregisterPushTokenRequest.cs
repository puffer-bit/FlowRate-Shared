using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Stops push notifications of this coordinator for a device of the sender. A token that is not
/// registered is not an error.
/// </summary>
public record UnregisterPushTokenRequest(
    [property: Required, NotBlank, StringLength(Limits.PushTokenMaxLength), NoControlCharacters] string Token)
    : CoordinatorRequest
{
    public UnregisterPushTokenResponse ToResponse() => new(RequestId);
}
