namespace Shared.Models.Requests;

public abstract record Request : BaseMessage
{
    public Guid RequestId { get; init; } = Guid.NewGuid();
}
