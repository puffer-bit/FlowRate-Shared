namespace Shared.Models.Media;

public record Inbound
{
    public required string Id { get; init; }
    public required int UserId { get; init; }
    public required uint SSRC { get; init; }
    public required MediaSourceType SourceType { get; init; }
    public required MediaType MediaType { get; init; }
    public required string MimeType { get; init; }
    public required int PayloadType { get; init; }
    public required int ClockRate { get; init; }
    public int Channels { get; init; }

    /// <summary>
    /// The SFU resends lost packets of the track when the client asks with a NACK, on the same SSRC.
    /// </summary>
    public bool IsNackEnabled { get; init; }

    /// <summary>How long the client holds the packets of the track before playing them, in ms; 0 for its own default.</summary>
    public int JitterBufferMs { get; init; }
}
