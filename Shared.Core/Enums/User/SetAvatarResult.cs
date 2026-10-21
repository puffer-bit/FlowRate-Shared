namespace Shared.Enums.User;

public enum SetAvatarResult
{
    InternalError = -1,
    NoError = 0,
    UserNotFound = 1,
    /// <summary>The upload is not a finished avatar upload of the sender.</summary>
    UploadNotFound = 2,
    /// <summary>Only the user themselves may put a picture on their profile.</summary>
    NotAllowed = 3,
    TimedOut = 4,
    Rejected = 5,
    /// <summary>The picture did not reach the server.</summary>
    UploadFailed = 6
}
