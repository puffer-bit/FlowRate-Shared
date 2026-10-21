namespace Shared.Models.Media;

public record SFUMediaInfo
{
    public int ClockRate { get; init; }
    public int PreferredPayloadType { get; init; }
    public MediaType MediaType { get; init; }
    public Dictionary<string, object> Parameters { get; init; } = new();
}
