namespace Shared.Models.Responses.Heartbeat;

public record HeartbeatPingResponse(
    Guid RequestId,
    long Timestamp) : HeartbeatResponse(RequestId);
