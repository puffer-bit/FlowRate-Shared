using System.ComponentModel.DataAnnotations;
using Shared.Models.Responses.WebRTC;
using Shared.Validation;

namespace Shared.Models.Requests.WebRTC;

public record WebRtcConnectRequest([property: EntityId] int SessionId, [property: Required, StringLength(Limits.SdpMaxLength)] string Data, bool IsGstWebRTC = false) : WebRtcRequest
{
    public WebRtcConnectResponse ToResponse(string? data)
        => new(RequestId, SessionId, data, IsGstWebRTC);
}
