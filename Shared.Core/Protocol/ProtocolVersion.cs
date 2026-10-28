using System.Globalization;

namespace Shared.Protocol;

/// <summary>
/// The version of the wire protocol. The client sends it in a header of the WebSocket upgrade request,
/// before any message, so the check does not depend on the message format it guards.
/// </summary>
public static class ProtocolVersion
{
    /// <summary>
    /// Raised by every change old clients or servers cannot understand: a removed or renamed field,
    /// a changed type or enum number, a request old servers do not know. Shared.Tests fails on any
    /// change of the message schema until it is looked at.
    /// </summary>
    // 2: a message carries files (NewChatMessageModel.Files is a list of NewChatFileModel);
    //    sessions carry permission overrides; ChangeSessionPermissionsRequest; STUN/TURN fields removed;
    //    coordinator settings carry upload and rate limits; HybridSessionSetAfkRequest, HybridSessionPokeRequest
    //    and the PokeMembers permission; the User* security requests (password, one-time codes and
    //    passkeys of the sender); Outbound.TransportCcExtensionId; the watching screen share notifications
    //    and PeerDTO.WatchedScreenShares
    // 3: device sessions: UserAuthRequest.SessionToken and Device, SessionToken in the auth responses;
    //    UserSessionsRequest, UserSessionRevokeRequest, UserIdentityResetRequest and
    //    CoordinatorUserSessionEndedNotification; a failed request is answered with ErrorResponse and one
    //    ErrorCode instead of a Result in every response (RequestRejectedResponse and the *Result enums
    //    removed, AuthResult keeps Success and SecondStageRequired)
    public const int Current = 3;

    /// <summary>The oldest client version the server still serves.</summary>
    public const int MinSupported = 3;

    /// <summary>
    /// Request header with the client version. The server answers a version it does not serve with
    /// 426 Upgrade Required and both its headers.
    /// </summary>
    public const string Header = "FlowRate-Protocol";

    /// <summary>Response header with <see cref="MinSupported"/> of the server.</summary>
    public const string MinHeader = "FlowRate-Protocol-Min";

    public static string Format(int version) => version.ToString(CultureInfo.InvariantCulture);

    public static bool TryParse(string? value, out int version) =>
        int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out version) && version > 0;

    /// <summary>Whether the server serves a client that sent this header value; no header is never served.</summary>
    public static bool IsSupported(string? clientHeader) =>
        TryParse(clientHeader, out var version) && version is >= MinSupported and <= Current;
}
