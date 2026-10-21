using System.Text;
using Shared.Models.Media.Transport;

namespace Shared.Tools.WebRTC;

[Obsolete("Produces an SDP that mediasoup cannot use: static payload types, no rtcp-mux and a " +
          "single m-section per media kind instead of one per producer or consumer. " +
          "Use Client.GStreamer.Services.Sdp.SdpBuilder instead.")]
public static class SdpParser
{
    public static string ParseToSdp(TransportData transportData)
    {
        var ice = transportData.ICEData;
        
        var fingerprint = transportData.DTLSData.Fingerprints
                              .FirstOrDefault(f => f.Algorithm.Equals("sha-256", StringComparison.OrdinalIgnoreCase)) 
                          ?? transportData.DTLSData.Fingerprints.First();
        
        string candidateStr = $"a=candidate:1 1 UDP 2130706431 {transportData.Host} {transportData.Port} typ host";

        var sdp = new StringBuilder();
        
        sdp.AppendLine("v=0");
        sdp.AppendLine("o=- 1234567 1 IN IP4 0.0.0.0");
        sdp.AppendLine("s=-");
        sdp.AppendLine("t=0 0");
        sdp.AppendLine("a=group:BUNDLE 0 1");
        sdp.AppendLine("a=msid-semantic: WMS");

        sdp.AppendLine($"m=audio {transportData.Port} UDP/TLS/RTP/SAVPF 100");
        sdp.AppendLine($"c=IN IP4 {transportData.Host}");
        sdp.AppendLine("a=rtpmap:100 opus/48000/2");
        sdp.AppendLine("a=fmtp:100 minptime=10;useinbandfec=1");
        sdp.AppendLine("a=mid:0");
        sdp.AppendLine("a=sendrecv");
        sdp.AppendLine($"a=ice-ufrag:{ice.UsernameFragment}");
        sdp.AppendLine($"a=ice-pwd:{ice.Password}");
        sdp.AppendLine("a=ice-options:trickle");
        sdp.AppendLine(candidateStr);
        sdp.AppendLine($"a=setup:passive");
        sdp.AppendLine($"a=fingerprint:{fingerprint.Algorithm.ToLower()} {fingerprint.Fingerprint}");
        
        sdp.AppendLine($"m=video {transportData.Port} UDP/TLS/RTP/SAVPF 96");
        sdp.AppendLine($"c=IN IP4 {transportData.Host}");
        sdp.AppendLine("a=rtpmap:96 VP8/90000");
        sdp.AppendLine("a=fmtp:96 packetization-mode=1;profile-level-id=42e01f;level-asymmetry-allowed=1");
        sdp.AppendLine("a=mid:1");
        sdp.AppendLine("a=sendrecv");
        sdp.AppendLine($"a=ice-ufrag:{ice.UsernameFragment}");
        sdp.AppendLine($"a=ice-pwd:{ice.Password}");
        sdp.AppendLine(candidateStr);
        sdp.AppendLine($"a=setup:passive");
        sdp.AppendLine($"a=fingerprint:{fingerprint.Algorithm.ToLower()} {fingerprint.Fingerprint}");

        return sdp.ToString();
    }
}