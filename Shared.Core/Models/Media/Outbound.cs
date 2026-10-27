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

    /// <summary>
    /// Id the SFU reads the transport-wide sequence number of the packets under, and answers with
    /// transport-cc feedback on, or 0 when it gives none. The same for every track of a server.
    /// </summary>
    public int TransportCcExtensionId { get; init; }

    /// <summary>
    /// An Opus track carries a copy of the previous packet in each one, to restore single losses.
    /// </summary>
    public bool IsFecEnabled { get; init; } = true;

    /// <summary>The loss, in percent, the Opus encoder prepares its FEC for.</summary>
    public int FecLossPercent { get; init; } = 10;

    /// <summary>A voice sends next to nothing while silent.</summary>
    public bool IsDtxEnabled { get; init; } = true;
}
