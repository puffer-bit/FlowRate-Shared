using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Shared.Validation;

namespace Shared.Models.DataTransferObjects;

/// <summary>
/// How a <see cref="Requests.Auth.LoginRequest"/> proves who it is. A device the account knows signs the challenge;
/// every other method adds a new device to the account, whose key it brings in <see cref="NewDeviceDTO"/>.
/// </summary>
[JsonPolymorphic]
[JsonDerivedType(typeof(DeviceLoginDTO), typeDiscriminator: "Device")]
[JsonDerivedType(typeof(RegisterLoginDTO), typeDiscriminator: "Register")]
[JsonDerivedType(typeof(RecoveryLoginDTO), typeDiscriminator: "Recovery")]
[JsonDerivedType(typeof(UsernameLoginDTO), typeDiscriminator: "Username")]
[JsonDerivedType(typeof(LoginCodeLoginDTO), typeDiscriminator: "LoginCode")]
[JsonDerivedType(typeof(LegacyLoginDTO), typeDiscriminator: "Legacy")]
public abstract record LoginMethodDTO;

/// <summary>A device of the account: <paramref name="Signature"/> signs <see cref="Auth.LoginSignature"/> of the challenge.</summary>
public record DeviceLoginDTO(
    [property: NotEmptyGuid] Guid DeviceId,
    [property: Required, StringLength(Limits.SignatureMaxLength), NoControlCharacters] string Signature) : LoginMethodDTO;

/// <summary>A new account, made by this device.</summary>
public record RegisterLoginDTO(
    [property: Required] NewDeviceDTO NewDevice,
    [property: Required] RecoveryKeysDTO Keys) : LoginMethodDTO;

/// <summary>
/// The recovery phrase: <paramref name="Signature"/> signs <see cref="Auth.LoginSignature"/> of the challenge and the new
/// device key with the recovery key, whose public part finds the account.
/// </summary>
public record RecoveryLoginDTO(
    [property: Required] NewDeviceDTO NewDevice,
    [property: Required, StringLength(Limits.PublicKeyMaxLength), NoControlCharacters] string RecoveryPublicKey,
    [property: Required, StringLength(Limits.SignatureMaxLength), NoControlCharacters] string Signature) : LoginMethodDTO;

/// <summary>The username; the user then proves a factor in the second stage.</summary>
public record UsernameLoginDTO(
    [property: Required] NewDeviceDTO NewDevice,
    [property: Required, StringLength(Limits.UsernameMaxLength), NoControlCharacters] string Username) : LoginMethodDTO;

/// <summary>A one-time login code the control panel gave out for the user.</summary>
public record LoginCodeLoginDTO(
    [property: Required] NewDeviceDTO NewDevice,
    [property: Required, StringLength(Limits.LoginCodeMaxLength), NoControlCharacters] string Code) : LoginMethodDTO;

/// <summary>
/// The identity a client kept before devices, with its session token when it had one. The account gets the device
/// and the recovery keys, and the identity stops working. A token the server still knows skips the second stage.
/// </summary>
public record LegacyLoginDTO(
    [property: Required] NewDeviceDTO NewDevice,
    [property: Required, StringLength(Limits.UserIdentityMaxLength), NoControlCharacters] string UserIdentity,
    [property: StringLength(Limits.SessionTokenMaxLength), NoControlCharacters] string? SessionToken,
    [property: Required] RecoveryKeysDTO Keys) : LoginMethodDTO;

/// <summary>The key of a device being added to an account.</summary>
/// <param name="PublicKey">P-256, SubjectPublicKeyInfo DER, base64url.</param>
/// <param name="IsTemporary">The device is not remembered: it ends after a day without a login.</param>
public record NewDeviceDTO(
    [property: Required, StringLength(Limits.PublicKeyMaxLength), NoControlCharacters] string PublicKey,
    bool IsTemporary = false);

/// <summary>
/// The public keys the recovery phrase of an account derives. The server keeps them and never sees the phrase.
/// </summary>
/// <param name="RecoveryPublicKey">Logs in with the phrase; P-256, SubjectPublicKeyInfo DER, base64url.</param>
/// <param name="AccountPublicKey">The identity key of the account for end-to-end encryption; kept for later.</param>
public record RecoveryKeysDTO(
    [property: Required, StringLength(Limits.PublicKeyMaxLength), NoControlCharacters] string RecoveryPublicKey,
    [property: Required, StringLength(Limits.PublicKeyMaxLength), NoControlCharacters] string AccountPublicKey);
