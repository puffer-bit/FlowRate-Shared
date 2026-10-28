namespace Shared.Enums;

/// <summary>
/// Why a request failed, carried by <see cref="Models.Responses.ErrorResponse"/>. One list for the whole
/// protocol: the request it answers tells what the code is about (<see cref="SessionNotFound"/> of a join
/// is the session to join). Codes are grouped by hundreds, so a new one goes at the end of its group.
/// </summary>
public enum ErrorCode
{
    // The request as a whole

    /// <summary>The server failed in a way it did not expect; the details are in its log.</summary>
    Internal = 1,

    /// <summary>The server did not get the work done in time, e.g. an SFU node did not answer.</summary>
    TimedOut = 2,

    /// <summary>Too many requests.</summary>
    RateLimited = 3,

    /// <summary>The request fails <see cref="Validation.MessageValidator"/>.</summary>
    InvalidRequest = 4,

    /// <summary>The sender's roles do not allow the request (see <see cref="Permission"/>).</summary>
    InsufficientPermissions = 5,

    /// <summary>The sender is frozen: they may read, but not write, join voice or change anything.</summary>
    UserFrozen = 6,

    /// <summary>The sender is not connected as a user of the coordinator.</summary>
    NotConnected = 7,

    /// <summary>The server does not offer this: media uploads, push or passkeys are not set up.</summary>
    FeatureDisabled = 8,

    // Logging in

    /// <summary>The identity, password, one-time code or passkey does not hold.</summary>
    AuthFailed = 100,

    Banned = 101,

    /// <summary>The coordinator already has as many users online as it takes.</summary>
    OnlineLimitReached = 102,

    /// <summary>The coordinator cannot register another user.</summary>
    UserLimitReached = 103,

    // What the request is about does not exist

    UserNotFound = 200,
    SessionNotFound = 201,
    PeerNotFound = 202,
    ChatNotFound = 203,
    MessageNotFound = 204,

    /// <summary>The message a new one replies to does not exist.</summary>
    ReplyTargetNotFound = 205,

    RoleNotFound = 206,
    CategoryNotFound = 207,
    TransportNotFound = 208,

    /// <summary>The upload is not a finished upload of the sender.</summary>
    UploadNotFound = 209,

    PasskeyNotFound = 210,

    // The state of things does not allow the request

    /// <summary>
    /// What the request changes was changed by someone else after the revision the change was based on;
    /// the client fetches it again.
    /// </summary>
    Conflict = 300,

    /// <summary>The name of a session, role or user is taken.</summary>
    NameTaken = 301,

    /// <summary>The same message was sent already.</summary>
    DuplicateMessage = 302,

    SessionFull = 303,

    /// <summary>A session with users in it cannot be deleted.</summary>
    SessionNotEmpty = 304,

    TooManyRoles = 305,
    TooManyCategories = 306,

    /// <summary>The session is not of the type the request is for, e.g. a voice request to a chat.</summary>
    WrongSessionType = 307,

    /// <summary>The sender is not in the session.</summary>
    NotMember = 308,

    /// <summary>The host rejected the peer, or has not approved them yet.</summary>
    NotAdmitted = 309,

    AlreadyApproved = 310,
    AlreadyRejected = 311,

    /// <summary>The voice, video or screen share to change or stop is not on.</summary>
    MediaNotActive = 312,

    /// <summary>The chat takes no new messages.</summary>
    ChatReadOnly = 313,

    /// <summary>Users cannot do this to themselves (poke, kick, ban, freeze, delete), nor to the system user.</summary>
    CannotTargetSelf = 314,

    /// <summary>The host of a session cannot be banned from it.</summary>
    TargetIsHost = 315,

    /// <summary>The role or the user is not below the sender's highest role.</summary>
    HierarchyViolation = 316,

    // Values the format allows but the server cannot use

    /// <summary>
    /// A value fits the format but cannot be used: a username, a status, a capacity, a role, the
    /// settings, DTLS parameters, a password... <see cref="Models.Responses.ErrorResponse.Fields"/> tells which.
    /// </summary>
    InvalidValue = 400,

    AttachmentsNotAllowed = 401,

    /// <summary>An attached upload has not finished.</summary>
    AttachmentNotReady = 402,

    AttachmentInvalid = 403,
    UnsupportedMedia = 404,

    // Account security

    /// <summary>The proof is missing or does not hold: the current password and/or one-time code.</summary>
    ProofRequired = 500,

    /// <summary>The code that turns one-time codes on, or the new passkey, does not hold.</summary>
    VerificationFailed = 501,

    // Media servers

    /// <summary>The SFU node failed the request.</summary>
    SfuError = 600,

    /// <summary>The session has no SFU node to work with.</summary>
    SfuUnavailable = 601
}
