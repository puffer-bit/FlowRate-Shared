using Shared.Enums;

namespace Shared.Models.Responses;

/// <summary>
/// Sent instead of the regular response when a request fails: the server refused it before handling
/// it, or handling it did not work out. The regular response is sent on success only.
/// </summary>
/// <param name="Details">What went wrong, for logs; not meant for end users.</param>
/// <param name="Fields">
/// Why values were refused, by field ("Heartbeat.TimeoutSeconds"; "" for the request as a whole);
/// with <see cref="ErrorCode.InvalidRequest"/> and <see cref="ErrorCode.InvalidValue"/>.
/// </param>
public record ErrorResponse(
    Guid RequestId,
    ErrorCode Code,
    string? Details = null,
    IDictionary<string, string[]>? Fields = null)
    : Response(RequestId);
