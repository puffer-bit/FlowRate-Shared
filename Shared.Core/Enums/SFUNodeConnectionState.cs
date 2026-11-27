namespace Shared.Enums;

/// <summary>How the server's connection to one SFU node is doing.</summary>
public enum SFUNodeConnectionState
{
    /// <summary>The first connection is being set up.</summary>
    Connecting = 0,

    /// <summary>The connection is up and the node answers pings.</summary>
    Connected = 1,

    /// <summary>The connection is up, but the node did not answer the last ping in time.</summary>
    Unresponsive = 2,

    /// <summary>The connection is down; the server reconnects by itself.</summary>
    Disconnected = 3
}
