namespace Shared.Models.Responses.WebRTC;

public abstract record WebRtcResponse(Guid RequestId) : Response(RequestId);
