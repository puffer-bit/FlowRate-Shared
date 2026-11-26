namespace Shared.Enums.User;

/// <summary>Who stands behind an account.</summary>
public enum UserKind
{
    /// <summary>A person, with the client.</summary>
    User = 0,

    /// <summary>
    /// A program on the client libraries, made by a user with <see cref="Permission.ManageBots"/>. It logs in with a
    /// login code and then its device key, takes no place among the online users or the registered ones (bots have
    /// their own limit), and has no recovery phrase, passwords, push notifications or idle status. Clients leave it out
    /// of the readers of a message.
    /// </summary>
    Bot = 1,

    /// <summary>
    /// A bot for testing and debugging, such as the test bot of the client: like <see cref="Bot"/> in everything,
    /// but it takes no place among the bots of the coordinator and clients mark it apart.
    /// </summary>
    DebugBot = 2
}

public static class UserKinds
{
    /// <summary>Whether the account is a bot of any kind.</summary>
    public static bool IsBot(this UserKind kind) => kind is UserKind.Bot or UserKind.DebugBot;
}
