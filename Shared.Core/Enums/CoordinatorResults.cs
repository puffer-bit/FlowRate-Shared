namespace Shared.Enums;

public enum UsersRequestResult
{
    InternalError = -1,
    NoError = 0,
    UserNotFound = 1,
    TimedOut = 2,
    Rejected = 3
}

public enum HandleMessageResult
{
    InternalError = -1,
    NoError = 0,
    UnexpectedError = 1,
    ForbiddenMessage = 2,
    ForbiddenRequest = 3,
    JsonParseError = 4,
    NoUserId = 5,
    GatewayNotFound = 6
}

public enum WebRTCNegotiationResult
{
    InternalError = -1,
    NoError = 0,
    UnexpectedError = 1
}
