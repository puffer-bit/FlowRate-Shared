namespace Shared.Enums;

public enum PushPlatform
{
    Android = 0,
    Ios = 1
}

public enum PushTokenResult
{
    InternalError = -1,
    NoError = 0,
    PushDisabled = 1,
    TimedOut = 2,
    Rejected = 3
}
