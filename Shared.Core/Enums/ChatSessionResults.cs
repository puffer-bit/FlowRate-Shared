namespace Shared.Enums;

public enum FetchMessagesResult
{
    InternalError = -1,
    NoError = 0,
    UnexpectedError = 1,
    ChatNotExists = 2,
    InvalidResponse = 3,
    TimedOut = 4,
    WrongResponse = 5,
    Rejected = 6
}

public enum FetchMessageResult
{
    InternalError = -1,
    NoError = 0,
    UnexpectedError = 1,
    ChatNotExists = 2,
    InvalidResponse = 3,
    MessageNotExists = 4,
    TimedOut = 5,
    WrongResponse = 6,
    Rejected = 7
}

public enum AddMessageResult
{
    InternalError = -1,
    NoError = 0,
    SameMessageAlreadyExist = 1,
    ChatNotExists = 2,
    TimedOut = 3,
    InvalidResponse = 4,
    WrongResponse = 5,
    ReplyTargetNotExists = 6,
    Rejected = 7,
    AttachmentsNotAllowed = 8,
    AttachmentNotReady = 9,
    AttachmentInvalid = 10
}

public enum EditMessageResult
{
    InternalError = -1,
    NoError = 0,
    MessageNotExists = 1,
    ChatNotExists = 2,
    InsufficientPermissions = 3,
    TimedOut = 4,
    Rejected = 5
}

public enum DeleteMessageResult
{
    InternalError = -1,
    NoError = 0,
    UnexpectedError = 1,
    MessageNotExists = 2,
    ChatNotExists = 3,
    TimedOut = 4,
    InvalidResponse = 5,
    WrongResponse = 6,
    InsufficientPermissions = 7,
    Rejected = 8
}

public enum ChatReadResult
{
    InternalError = -1,
    NoError = 0,
    ChatNotExists = 1,
    TimedOut = 2,
    Rejected = 3
}
