using System.Diagnostics.CodeAnalysis;
using Shared.Models.Responses.Heartbeat;

namespace Shared.Models.Requests.Heartbeat;

public record HeartbeatPingRequest() : HeartbeatRequest
{
    public HeartbeatPingResponse ToResponse(long timestamp)
        => new(RequestId, timestamp);
}
