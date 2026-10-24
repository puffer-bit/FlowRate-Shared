namespace Shared.Validation;

/// <summary>
/// Size limits of request fields, shared so the client can check input before sending and the
/// server rejects the same things. String limits match the server database columns.
/// </summary>
public static class Limits
{
    public const int UserIdentityMaxLength = 37;
    public const int PasswordMaxLength = 128;
    public const int TwoFactorMaxLength = 6;
    public const int Fido2FieldMaxLength = 4096;
    public const int Fido2UserHandleMaxLength = 128;
    public const int UserIdsMaxCount = 100;
    public const int UsernameMaxLength = 32;
    public const int UserSearchMaxCount = 20;

    public const int RoleNameMaxLength = 32;
    public const int RolesMaxCount = 100;
    public const int RoleColorMax = 0xFFFFFF;

    public const int SessionNameMaxLength = 50;
    public const int SessionCapacityMin = 1;
    public const int SessionCapacityMax = 100;
    public const int SessionDescriptionMaxLength = 500;
    public const int SessionCategoryNameMaxLength = 50;
    public const int SessionCategoriesMaxCount = 50;
    public const int SessionLayoutIndexMax = 1000;
    public const int UrlMaxLength = 2048;
    public const int VideoBitrateMinKbps = 100;
    public const int VideoBitrateMaxKbps = 20_000;
    public const int KeyFrameRequestDelayMinMs = 0;
    public const int KeyFrameRequestDelayMaxMs = 10_000;
    public const int JitterBufferMinMs = 20;
    public const int JitterBufferMaxMs = 2000;

    public const int ReasonMaxLength = 200;

    public const int ChatTextMaxLength = 4000;
    public const int ChatHistoryMaxTake = 100;
    public const int ChatAttachmentsMaxCount = 10;
    public const int ChatAttachmentDescriptionMaxLength = 500;
    public const int ChatImageMaxSide = 16384;
    public const int ChatImagePreviewMaxSize = 1024 * 1024;
    public const int AvatarMaxSize = 1024 * 1024;
    public const int FileNameMaxLength = 255;
    public const int MimeTypeMaxLength = 100;

    // Bytes of the random id an upload is known by, written in hex
    public const int MediaUploadIdLength = 16;

    /// <summary>The image formats a message may carry.</summary>
    public static readonly IReadOnlyList<string> ChatImageMimeTypes =
        ["image/jpeg", "image/png", "image/webp", "image/gif"];
    public static readonly TimeSpan ChatMessageMaxDelay = TimeSpan.FromDays(30);
    public static readonly TimeSpan ChatMessageMaxLifetime = TimeSpan.FromDays(365);

    public const int ClientEpochMaxLength = 36;
    public const int ResumedInboundsMaxCount = 256;
    public const int DtlsFingerprintsMaxCount = 5;
    public const int DtlsAlgorithmMaxLength = 16;

    // sha-512 is 64 bytes written as colon-separated hex pairs
    public const int DtlsFingerprintMaxLength = 64 * 3 - 1;

    public const int SdpMaxLength = 64 * 1024;

    public const int PushTokenMaxLength = 512;

    // Bytes: an AES-256 key and the random id the device knows it by
    public const int PushKeyLength = 32;
    public const int PushKeyIdLength = 16;
    public const int PushKeyBase64Length = (PushKeyLength + 2) / 3 * 4;

    // Bytes of the token a device answers notifications with
    public const int PushReplyTokenLength = 32;

    public const int CoordinatorNameMaxLength = 100;
    public const int MotdTextMaxLength = 2000;
    // The longest IPv6 address written out, with an IPv4 tail
    public const int IpAddressMaxLength = 45;
    public const int IpListMaxCount = 1000;
    public const int SfuNodesMaxCount = 100;

    public const int UnattachedUploadsMaxCount = 1000;
    public const int UnattachedUploadLifetimeMaxHours = 24 * 30;
    public const int MediaTokenLifetimeMaxMinutes = 24 * 60;

    public const int RequestRateCountMax = 1_000_000;
    public const double RequestRatePeriodMinSeconds = 0.001;
    public const double RequestRatePeriodMaxSeconds = 86_400;
    public const int RequestRejectionsMax = 1_000_000;
    public const int RequestRejectionWindowMaxSeconds = 86_400;
    public const int ConnectionsPerIpMax = 1_000_000;
}
