using Shared.Enums;

namespace Shared.Models.Responses.WebRTC;

public record WebRtcConnectResponse(
    Guid RequestId,
    int SessionId,
    string? Data,
    WebRTCNegotiationResult Result,
    bool IsGstWebRTC) : WebRtcResponse(RequestId);
