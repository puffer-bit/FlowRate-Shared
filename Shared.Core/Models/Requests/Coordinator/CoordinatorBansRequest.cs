using System.ComponentModel.DataAnnotations;
using Shared.Enums.User;
using Shared.Validation;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;

namespace Shared.Models.Requests.Coordinator;

/// <summary>The active bans of the coordinator, for users who may ban.</summary>
public record CoordinatorBansRequest : CoordinatorRequest
{
    public CoordinatorBansResponse ToResponse(List<BanDTO> bans) => new(RequestId, bans);
}

/// <summary>
/// Bans another user. An <see cref="BanKind.Account"/> ban keeps the account from logging in; an
/// <see cref="BanKind.Ip"/> ban keeps everyone off <paramref name="Address"/>, by default the address the user
/// last connected from, and is listed under the user. A banned user is disconnected.
/// </summary>
/// <param name="ExpiresAt">When the ban ends; null for a permanent one.</param>
public record CoordinatorBanRequest(
    [property: EntityId] int TargetUserId,
    [property: DefinedEnum] BanKind Kind,
    [property: StringLength(Limits.BanReasonMaxLength), NoControlCharacters(allowLineBreaks: true)] string? Reason = null,
    DateTimeOffset? ExpiresAt = null,
    [property: StringLength(Limits.IpAddressMaxLength)] string? Address = null) : CoordinatorRequest
{
    public CoordinatorBanResponse ToResponse(BanDTO ban, UserDTO user) => new(RequestId, ban, user);
}

/// <summary>Lifts a ban; an account ban gives the user back the state they had before it.</summary>
public record CoordinatorRevokeBanRequest([property: NotEmptyGuid] Guid BanId) : CoordinatorRequest
{
    public CoordinatorRevokeBanResponse ToResponse() => new(RequestId);
}
