namespace Shared.Enums;

public enum HybridSessionNodeState
{
    /// <summary>The session runs on an SFU node and peers can join.</summary>
    Active = 0,

    /// <summary>
    /// No SFU node of the pool is available. Peers stay in the session and rejoin by themselves
    /// once a node is assigned (<c>HybridSessionStateChangedNotification</c> with <see cref="Active"/>).
    /// </summary>
    WaitingForSFUNode = 1,
}
