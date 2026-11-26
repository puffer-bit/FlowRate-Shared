using Shared.Enums.User;

namespace Shared.Models.Responses.Coordinator;

/// <param name="Level">The level of the sending device, as it is now.</param>
public record CoordinatorSetIdleResponse(
    Guid RequestId,
    IdleLevel Level) : CoordinatorResponse(RequestId);
