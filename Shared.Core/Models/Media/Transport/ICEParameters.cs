namespace Shared.Models.Media.Transport;

public record ICEParameters
{
    public required string UsernameFragment { get; init; }
    public required string Password { get; init; }
    public required List<ICECandidate> Candidates { get; init; }
}
