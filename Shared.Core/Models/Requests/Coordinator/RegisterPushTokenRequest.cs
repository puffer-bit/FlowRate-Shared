using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>
/// Registers the device of the sender for push notifications of this coordinator. The content of
/// every push is encrypted with <paramref name="Key"/> (AES-256-GCM, base64) and carries
/// <paramref name="KeyId"/> in the clear, so the device finds the key without revealing the coordinator.
/// A token registered again replaces its earlier registration, whoever made it.
/// </summary>
public record RegisterPushTokenRequest(
    [property: Required, NotBlank, StringLength(Limits.PushTokenMaxLength), NoControlCharacters] string Token,
    [property: DefinedEnum] PushPlatform Platform,
    [property: Required, StringLength(Limits.PushKeyIdLength * 2), HexBytes(Limits.PushKeyIdLength)] string KeyId,
    [property: Required, StringLength(Limits.PushKeyBase64Length), Base64Bytes(Limits.PushKeyLength)] string Key) : CoordinatorRequest
{
    public RegisterPushTokenResponse ToResponse(string? replyToken = null) =>
        new(RequestId, replyToken);
}
