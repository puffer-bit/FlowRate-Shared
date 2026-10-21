namespace Shared.Enums;

public enum HybridSessionPeerState
{
    New = 0,
    WaitingForApprove = 1,
    Connecting = 3,
    WaitingForServer = 4,
    Connected = 5,
    Closed = 6,
}
