namespace Shared.Models.DataTransferObjects;

public sealed class FIDO2LoginOptionsDTO
{
    public string Challenge { get; set; } = string.Empty;
    public string RpId { get; set; } = string.Empty;
    public long Timeout { get; set; } = 60000;
    public List<FIDO2AllowedCredentialDTO> AllowedCredentials { get; set; } = new();
    public string UserVerification { get; set; } = "required";
}

public sealed class FIDO2AllowedCredentialDTO
{
    public string Type { get; set; } = "public-key";
    public string Id { get; set; } = string.Empty;
}