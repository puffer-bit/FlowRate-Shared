namespace Shared.Models.Responses;

public abstract record Response(Guid RequestId) : BaseMessage;
