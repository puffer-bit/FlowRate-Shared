using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorBansResponse(Guid RequestId, List<BanDTO> Bans) : CoordinatorResponse(RequestId);

/// <param name="User">The banned user as they are now.</param>
public record CoordinatorBanResponse(Guid RequestId, BanDTO Ban, UserDTO User) : CoordinatorResponse(RequestId);

public record CoordinatorRevokeBanResponse(Guid RequestId) : CoordinatorResponse(RequestId);
