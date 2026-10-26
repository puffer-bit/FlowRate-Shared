using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.Responses.SessionActions.HybridSession;
using Shared.Validation;

namespace Shared.Models.Requests.SessionActions.HybridSession;

/// <summary>Pokes another peer of the session, with a message if there is one; only the poked user hears of it.</summary>
public record HybridSessionPokeRequest(
    [property: EntityId] int SessionId,
    [property: EntityId] int UserTargetId,
    [property: StringLength(Limits.PokeMessageMaxLength), NoControlCharacters] string? Message = null)
    : HybridSessionRequest
{
    public HybridSessionPokeResponse ToResponse(PokeResult result)
        => new(RequestId, result, UserTargetId);
}
