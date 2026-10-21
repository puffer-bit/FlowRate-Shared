using Shared.Enums;

namespace Shared.Models.Responses;

/// <summary>
/// Sent instead of the regular response when the server refuses a request before handling it.
/// </summary>
/// <param name="Details">What was exceeded or invalid, for logs; not meant for end users.</param>
public record RequestRejectedResponse(
    Guid RequestId,
    RequestRejectReason Reason,
    string? Details = null)
    : Response(RequestId);
