namespace Shared.Models.Responses.WebRTC;

public record WebRtcConnectResponse(
    Guid RequestId,
    int SessionId,
    string? Data,
    bool IsGstWebRTC) : WebRtcResponse(RequestId);
