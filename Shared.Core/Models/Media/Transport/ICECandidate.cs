namespace Shared.Models.Media.Transport;

public record ICECandidate
{
    public required string Candidate { get; init; }
    public required string SDPMid { get; init; }
    public required int SDPMLineIndex { get; init; }
}
