using System.ComponentModel.DataAnnotations;
using Shared.Validation;
namespace Shared.Models.Media.Transport;

public record DTLSFingerprint
{
    [Required, OneOf("sha-1", "sha-224", "sha-256", "sha-384", "sha-512")]
    public required string Algorithm { get; init; }
    [Required, StringLength(Limits.DtlsFingerprintMaxLength), DtlsFingerprint]
    public required string Fingerprint { get; init; }
}
