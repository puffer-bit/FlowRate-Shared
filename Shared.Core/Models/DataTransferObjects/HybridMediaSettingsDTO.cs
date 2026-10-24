using System.ComponentModel.DataAnnotations;
using Shared.Enums.WebRTC;
using Shared.Validation;

namespace Shared.Models.DataTransferObjects;

/// <summary>
/// How the media of a hybrid session travel: codecs and the ways lost packets are made up for.
/// Bitrate, frame rate and resolution are not here; the sender picks them when a stream starts.
/// The defaults are what the client and the SFU use when nothing else is said.
/// </summary>
/// <remarks>
/// The settings are stored and shown, but not yet applied: streams keep the defaults whatever
/// the session says.
/// </remarks>
public record HybridMediaSettingsDTO : IValidatableObject
{
    /// <summary>The video codecs a session may pick; the SFU knows no other.</summary>
    public static readonly IReadOnlyList<WebRTCVideoCodecs> SupportedVideoCodecs =
        [WebRTCVideoCodecs.VP8, WebRTCVideoCodecs.H264];

    /// <summary>The audio codecs a session may pick; the SFU knows no other.</summary>
    public static readonly IReadOnlyList<WebRTCAudioCodecs> SupportedAudioCodecs = [WebRTCAudioCodecs.Opus];

    [DefinedEnum] public WebRTCVideoCodecs CameraCodec { get; init; } = WebRTCVideoCodecs.VP8;
    [DefinedEnum] public WebRTCVideoCodecs ScreenCodec { get; init; } = WebRTCVideoCodecs.H264;

    /// <summary>The codec of voices and of the sound of screen shares.</summary>
    [DefinedEnum] public WebRTCAudioCodecs AudioCodec { get; init; } = WebRTCAudioCodecs.Opus;

    /// <summary>Receivers of video ask for lost packets again instead of waiting for a keyframe.</summary>
    public bool IsVideoNackEnabled { get; init; } = true;

    /// <summary>Lost video packets come again on a stream of their own (RTX) rather than on the original one.</summary>
    public bool IsVideoRtxEnabled { get; init; } = true;

    /// <summary>How long the SFU waits before passing on another keyframe request of the same stream.</summary>
    [Range(Limits.KeyFrameRequestDelayMinMs, Limits.KeyFrameRequestDelayMaxMs)]
    public int KeyFrameRequestDelayMs { get; init; } = 1000;

    /// <summary>Receivers of audio ask for lost packets again.</summary>
    public bool IsAudioNackEnabled { get; init; }

    /// <summary>Opus carries a copy of the previous packet in each one to restore single losses (in-band FEC).</summary>
    public bool IsOpusFecEnabled { get; init; } = true;

    /// <summary>The loss Opus prepares its FEC for; more loss spends more bitrate on the copies.</summary>
    [Range(0, 100)]
    public int OpusPacketLossPercent { get; init; } = 10;

    /// <summary>Voices send next to nothing while silent (DTX).</summary>
    public bool IsVoiceDtxEnabled { get; init; } = true;

    /// <summary>How long receivers hold voice packets to put them in order and wait for late ones.</summary>
    [Range(Limits.JitterBufferMinMs, Limits.JitterBufferMaxMs)]
    public int VoiceJitterBufferMs { get; init; } = 100;

    /// <summary>How long receivers hold video packets to put them in order and wait for repaired ones.</summary>
    [Range(Limits.JitterBufferMinMs, Limits.JitterBufferMaxMs)]
    public int VideoJitterBufferMs { get; init; } = 200;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!SupportedVideoCodecs.Contains(CameraCodec))
            yield return new ValidationResult($"{nameof(CameraCodec)} {CameraCodec} is not supported.");
        if (!SupportedVideoCodecs.Contains(ScreenCodec))
            yield return new ValidationResult($"{nameof(ScreenCodec)} {ScreenCodec} is not supported.");
        if (!SupportedAudioCodecs.Contains(AudioCodec))
            yield return new ValidationResult($"{nameof(AudioCodec)} {AudioCodec} is not supported.");
    }
}
