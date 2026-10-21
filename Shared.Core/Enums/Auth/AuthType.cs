namespace Shared.Enums.Auth;

public enum AuthResult
{
    Success,
    AuthFailed,
    Banned,
    SecondStageRequeried,
    /// <summary>The coordinator already has as many users online as it takes.</summary>
    OnlineLimitReached,
    /// <summary>The coordinator cannot register another user.</summary>
    UserLimitReached
}

public enum SecondStageAuthType
{
    FIDO2,
    TwoFactor,
    Password
}
