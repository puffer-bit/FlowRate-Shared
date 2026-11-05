using Shared.Validation;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.SessionActions.ChatSession;

namespace Shared.Models.Requests.SessionActions.ChatSession;

/// <summary>Subscribes to the chat audience, or closes the subscription of this device.</summary>
public record ChatViewersRequest([property: EntityId] int ChatId, Guid SubscriptionId,
    bool Subscribe = true) : ChatSessionRequest
{
    public ChatViewersResponse ToResponse(List<UserDTO> users) => new(RequestId, users);
}
