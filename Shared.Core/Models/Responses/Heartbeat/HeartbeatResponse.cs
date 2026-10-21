namespace Shared.Models.Responses.Heartbeat;

public abstract record HeartbeatResponse(Guid RequestId) : Response(RequestId);
