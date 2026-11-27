using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

/// <summary>Marks the sender as away from the keyboard in the session, or back; the reason goes only with away.</summary>
public record HybridSessionSetAfkRequest(
    [property: EntityId] int SessionId,
    bool IsAfk,
    [property: StringLength(Limits.ReasonMaxLength), NoControlCharacters] string? Reason = null) : HybridSessionRequest
{
    public HybridSessionSetAfkResponse ToResponse()
        => new(RequestId, IsAfk);
}
