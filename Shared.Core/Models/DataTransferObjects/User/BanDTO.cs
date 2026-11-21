using Shared.Enums.User;

namespace Shared.Models.DataTransferObjects;

/// <param name="UserId">
/// The banned user; for an IP ban, the user it was made for. Null only for IP bans carried over from the
/// address list of older servers.
/// </param>
public record BanDTO(Guid Id, BanKind Kind, int? UserId, string? Address, string Reason,
    int CreatedBy, DateTime CreatedAt, DateTime? ExpiresAt);
