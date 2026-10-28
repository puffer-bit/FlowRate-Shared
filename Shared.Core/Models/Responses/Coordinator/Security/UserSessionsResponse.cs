using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator.Security;

/// <param name="Sessions">The sessions of the sender, the latest used first; null unless the result is NoError.</param>
public record UserSessionsResponse(
    Guid RequestId,
    List<UserSessionDTO> Sessions) : CoordinatorResponse(RequestId);
