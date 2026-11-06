using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorBansResponse(Guid RequestId, List<BanDTO> Bans) : CoordinatorResponse(RequestId);
public record CoordinatorRevokeBanResponse(Guid RequestId) : CoordinatorResponse(RequestId);
