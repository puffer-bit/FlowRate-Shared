using System.ComponentModel.DataAnnotations;
using Shared.Validation;

namespace Shared.Models.DataTransferObjects;

public class FIDO2AssertionDTO
{
    [StringLength(Limits.FIDO2FieldMaxLength)] public string CredentialId { get; set; } = string.Empty;
    [StringLength(Limits.FIDO2FieldMaxLength)] public string ClientDataJson { get; set; } = string.Empty;
    [StringLength(Limits.FIDO2FieldMaxLength)] public string AuthenticatorData { get; set; } = string.Empty;
    [StringLength(Limits.FIDO2FieldMaxLength)] public string Signature { get; set; } = string.Empty;
    [StringLength(Limits.FIDO2UserHandleMaxLength)] public string? UserHandle { get; set; }
}