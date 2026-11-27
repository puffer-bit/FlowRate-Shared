namespace Shared.Enums.User;

public enum BanKind
{
    Account = 0,
    Ip = 1,
    // Reserved for a future implementation. Servers reject this kind for now.
    Hardware = 2
}
