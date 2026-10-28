using Shared.Enums;
using Shared.Models.Responses;

namespace Shared.Models.Requests;

public abstract record Request : BaseMessage
{
    public Guid RequestId { get; init; } = Guid.NewGuid();

    /// <summary>The answer when the request fails, instead of its regular response.</summary>
    public ErrorResponse ToError(ErrorCode code, string? details = null, IDictionary<string, string[]>? fields = null)
        => new(RequestId, code, details, fields);
}
