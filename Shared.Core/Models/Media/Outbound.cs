namespace Shared.Models.Media;

public record Outbound
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
    /// Payload type of the stream the client retransmits lost packets of the track on (RFC 4588),
    /// or 0 when the track has none and lost packets are not resent.
    /// </summary>
    public int RtxPayloadType { get; init; }

    /// <summary>SSRC of the retransmission stream, or 0 when the track has none.</summary>
    public uint RtxSSRC { get; init; }
}
