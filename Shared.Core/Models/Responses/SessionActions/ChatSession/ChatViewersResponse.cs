using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.SessionActions.ChatSession;

public record ChatViewersResponse(Guid RequestId, List<UserDTO> Users) : ChatSessionResponse(RequestId);
