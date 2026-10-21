using System.ComponentModel.DataAnnotations;
using Shared.Validation;
namespace Shared.Models.Media.Transport;

public record DTLSParameters
{
    [Required, OneOf("auto", "client", "server")]
    public required string Role { get; init; }
    [Required, ItemCount(1, Limits.DtlsFingerprintsMaxCount)]
    public required List<DTLSFingerprint> Fingerprints { get; init; }
}
