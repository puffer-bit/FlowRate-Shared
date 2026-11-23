using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

namespace Shared.Auth;

/// <summary>
/// What a device or the recovery key signs to log in, and how the server checks it. Keys are P-256: public keys travel
/// as SubjectPublicKeyInfo DER, signatures as r‖s (IEEE P1363, 64 bytes), both base64url.
/// </summary>
public static class LoginSignature
{
    public const string DevicePurpose = "device";
    public const string RecoveryPurpose = "recovery";

    /// <summary>
    /// The bytes to sign: the purpose, the coordinator and the challenge it gave this connection, and the key of the
    /// device being added, so a signature is good for one login on one coordinator only.
    /// </summary>
    public static byte[] Payload(string purpose, string coordinatorId, string nonce, string? newDevicePublicKey = null) =>
        Encoding.UTF8.GetBytes($"FlowRate login v1\n{purpose}\n{coordinatorId}\n{nonce}\n{newDevicePublicKey}");

    /// <summary>The public key as it travels, or null when it is not a P-256 SubjectPublicKeyInfo.</summary>
    public static byte[]? TryDecodePublicKey(string? publicKey)
    {
        var bytes = TryDecode(publicKey);
        if (bytes == null)
            return null;

        try
        {
            using var key = ECDsa.Create();
            key.ImportSubjectPublicKeyInfo(bytes, out var read);
            if (read != bytes.Length || key.KeySize != 256)
                return null;
            return bytes;
        }
        catch (CryptographicException)
        {
            return null;
        }
    }

    /// <summary>Whether <paramref name="signature"/> signs <paramref name="payload"/> with the key.</summary>
    public static bool Verify(byte[] publicKey, byte[] payload, string? signature)
    {
        var signatureBytes = TryDecode(signature);
        if (signatureBytes is not { Length: 64 })
            return false;

        try
        {
            using var key = ECDsa.Create();
            key.ImportSubjectPublicKeyInfo(publicKey, out _);
            return key.VerifyData(payload, signatureBytes, HashAlgorithmName.SHA256,
                DSASignatureFormat.IeeeP1363FixedFieldConcatenation);
        }
        catch (CryptographicException)
        {
            return false;
        }
    }

    public static string Encode(ReadOnlySpan<byte> bytes) => Base64Url.EncodeToString(bytes);

    public static byte[]? TryDecode(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return null;
        try
        {
            return Base64Url.DecodeFromChars(value);
        }
        catch (FormatException)
        {
            return null;
        }
    }
}
