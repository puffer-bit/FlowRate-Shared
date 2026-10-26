using System.ComponentModel.DataAnnotations;
using Shared.Validation;

namespace Shared.Models.DataTransferObjects;

/// <summary>What the user logs in with besides the identity. Secrets are never sent.</summary>
/// <param name="IsTwoFactorPending">A secret was offered and waits for its first code.</param>
/// <param name="PasskeysAvailable">False when the server has no passkeys set up; none can be added.</param>
public record UserSecurityDTO(
    bool HasPassword,
    bool IsTwoFactorEnabled,
    bool IsTwoFactorPending,
    bool PasskeysAvailable,
    List<UserPasskeyDTO> Passkeys);

public record UserPasskeyDTO(
    int Id,
    string Label,
    DateTime CreatedAtUtc,
    DateTime? LastUsedAtUtc);

/// <summary>
/// The current password and one-time code of the sender, asked for by every change while the user has them, so an
/// open session alone is not enough to take over the account. Each part is left null when the user does not have it.
/// </summary>
public record UserSecurityProofDTO(
    [property: StringLength(Limits.PasswordMaxLength)] string? Password,
    [property: StringLength(Limits.TwoFactorMaxLength), NoControlCharacters] string? TwoFactorCode);
