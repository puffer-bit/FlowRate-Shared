using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator;

public record CoordinatorBotsResponse(Guid RequestId, List<UserDTO> Bots) : CoordinatorResponse(RequestId);

public record CoordinatorCreateBotResponse(Guid RequestId, UserDTO Bot, BotLoginCodeDTO LoginCode)
    : CoordinatorResponse(RequestId);

public record CoordinatorBotLoginCodeResponse(Guid RequestId, int TargetUserId, BotLoginCodeDTO LoginCode)
    : CoordinatorResponse(RequestId);

public record CoordinatorDeleteBotResponse(Guid RequestId, int TargetUserId) : CoordinatorResponse(RequestId);
