using System.Security.Cryptography;
using System.Text.Json;
using Shared.Auth;
using Shared.Models;
using Shared.Models.DataTransferObjects;
using Shared.Models.Requests.Auth;
using Shared.Serialization;
using Xunit;

namespace Shared.Tests;

public class LoginSignatureTests
{
    [Fact]
    public void Verify_TakesTheSignatureOfThePayloadWithTheKey()
    {
        using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        var publicKey = LoginSignature.TryDecodePublicKey(LoginSignature.Encode(key.ExportSubjectPublicKeyInfo()));
        Assert.NotNull(publicKey);

        var payload = LoginSignature.Payload(LoginSignature.DevicePurpose, "coordinator", "nonce");
        var signature = LoginSignature.Encode(key.SignData(payload, HashAlgorithmName.SHA256,
            DSASignatureFormat.IeeeP1363FixedFieldConcatenation));

        Assert.True(LoginSignature.Verify(publicKey!, payload, signature));
        Assert.False(LoginSignature.Verify(publicKey!,
            LoginSignature.Payload(LoginSignature.DevicePurpose, "coordinator", "other"), signature));
        Assert.False(LoginSignature.Verify(publicKey!,
            LoginSignature.Payload(LoginSignature.RecoveryPurpose, "coordinator", "nonce"), signature));
        Assert.False(LoginSignature.Verify(publicKey!, payload, "not base64url!"));
    }

    [Fact]
    public void TryDecodePublicKey_RefusesOtherCurvesAndGarbage()
    {
        using var p384 = ECDsa.Create(ECCurve.NamedCurves.nistP384);
        Assert.Null(LoginSignature.TryDecodePublicKey(LoginSignature.Encode(p384.ExportSubjectPublicKeyInfo())));
        Assert.Null(LoginSignature.TryDecodePublicKey("AAAA"));
        Assert.Null(LoginSignature.TryDecodePublicKey(null));
    }

    [Fact]
    public void LoginMethod_TravelsWithItsKind()
    {
        BaseMessage request = new LoginRequest("pw", new UsernameLoginDTO(new NewDeviceDTO("key", true), "alice"));

        var json = JsonSerializer.Serialize(request, ProtocolJsonContext.Default.BaseMessage);
        var read = Assert.IsType<LoginRequest>(JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage));

        var method = Assert.IsType<UsernameLoginDTO>(read.Method);
        Assert.Equal("alice", method.Username);
        Assert.True(method.NewDevice.IsTemporary);
        Assert.Contains("\"$type\":\"Username\"", json);
    }
}
