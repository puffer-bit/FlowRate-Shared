namespace Shared.Models.Media;

/// <summary>
/// The HTTP side of the coordinator, served on the same address and port as its web socket.
/// Every request carries the media token as a bearer token.
/// </summary>
public static class MediaEndpoints
{
    /// <summary>Uploads by the tus protocol, version 1.0.0.</summary>
    public const string Uploads = "/media/uploads";

    /// <summary>
    /// <c>GET {Images}/{id}?variant=...</c> returns an image of a message; byte ranges are supported.
    /// </summary>
    public const string Images = "/media/images";

    /// <summary>
    /// <c>GET {Avatars}/{id}</c> returns the avatar a user has; <see cref="DataTransferObjects.UserDTO.AvatarUrl"/>
    /// holds this path. A new avatar has another id, so what was fetched once never changes.
    /// </summary>
    public const string Avatars = "/media/avatars";

    public const string VariantParameter = "variant";
    public const string VariantOriginal = "original";
    public const string VariantPreview = "preview";
}

/// <summary>
/// The keys of the tus <c>Upload-Metadata</c> header an upload is created with.
/// </summary>
public static class MediaUploadMetadata
{
    /// <summary>
    /// The chat the upload is meant for; it can be attached to a message of that chat only.
    /// An avatar has none.
    /// </summary>
    public const string ChatId = "chatId";

    public const string MimeType = "mimeType";

    /// <summary><see cref="KindImage"/>, <see cref="KindImagePreview"/> or <see cref="KindAvatar"/>.</summary>
    public const string Kind = "kind";

    public const string KindImage = "image";
    public const string KindImagePreview = "imagePreview";

    /// <summary>A picture a user puts on their profile with <c>CoordinatorSetAvatarRequest</c>.</summary>
    public const string KindAvatar = "avatar";
}
