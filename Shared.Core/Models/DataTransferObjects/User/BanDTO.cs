using Shared.Enums.User;

namespace Shared.Models.DataTransferObjects;

public record BanDTO(Guid Id, BanKind Kind, int? UserId, string? Address, string Reason,
    int CreatedBy, DateTime CreatedAt, DateTime? ExpiresAt);
