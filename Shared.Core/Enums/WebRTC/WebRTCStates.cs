namespace Shared.Enums.WebRTC;

public enum WebRTCICEConnectionState
{
    New = 0,
    Checking = 1,
    Connected = 2,
    Completed = 3,
    Failed = 4,
    Disconnected = 5,
    Closed = 6
}

public enum WebRTCConnectionState
{
    New = 0,
    Connecting = 1,
    Connected = 2,
    Disconnected = 3,
    Failed = 4,
    Closed = 5,
    Fatal = 6
}
