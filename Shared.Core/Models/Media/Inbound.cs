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
}
