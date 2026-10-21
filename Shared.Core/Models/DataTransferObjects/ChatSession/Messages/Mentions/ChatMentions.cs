namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Mentions;

public enum ChatMentionKind
{
    User,
    Everyone,
    Here
}

/// <summary>
/// A mention token in the text of a message: where it sits and whom it points at.
/// <see cref="UserId"/> is set only for <see cref="ChatMentionKind.User"/>.
/// </summary>
public readonly record struct ChatMentionToken(int Start, int Length, ChatMentionKind Kind, int UserId)
{
    public int End => Start + Length;
}

/// <summary>
/// Mentions are written into the text as tokens: <c>&lt;@42&gt;</c> for a user,
/// <c>&lt;@everyone&gt;</c> for everyone in the chat and <c>&lt;@here&gt;</c> for those online.
/// The text keeps the id, so a renamed user stays mentioned; clients show the current name.
/// </summary>
public static class ChatMentions
{
    public const string EveryoneToken = "<@everyone>";
    public const string HereToken = "<@here>";

    // A longer id does not fit into an int
    private const int MaxIdDigits = 10;

    public static string UserToken(int userId) => $"<@{userId}>";

    public static IReadOnlyList<ChatMentionToken> Find(string? text)
    {
        if (string.IsNullOrEmpty(text))
            return [];

        List<ChatMentionToken>? tokens = null;
        int position = 0;

        while (position < text.Length)
        {
            int start = text.IndexOf("<@", position, StringComparison.Ordinal);
            if (start < 0)
                break;

            if (TryRead(text, start, out var token))
            {
                (tokens ??= []).Add(token);
                position = token.End;
            }
            else
            {
                position = start + 2;
            }
        }

        return tokens ?? (IReadOnlyList<ChatMentionToken>)[];
    }

    /// <summary>
    /// The users the text mentions by id, each once, in the order they first appear.
    /// </summary>
    public static List<int> UserIdsOf(IReadOnlyList<ChatMentionToken> tokens)
    {
        var ids = new List<int>();
        foreach (var token in tokens)
        {
            if (token.Kind == ChatMentionKind.User && !ids.Contains(token.UserId))
                ids.Add(token.UserId);
        }
        return ids;
    }

    private static bool TryRead(string text, int start, out ChatMentionToken token)
    {
        token = default;

        if (string.CompareOrdinal(text, start, EveryoneToken, 0, EveryoneToken.Length) == 0)
        {
            token = new ChatMentionToken(start, EveryoneToken.Length, ChatMentionKind.Everyone, 0);
            return true;
        }

        if (string.CompareOrdinal(text, start, HereToken, 0, HereToken.Length) == 0)
        {
            token = new ChatMentionToken(start, HereToken.Length, ChatMentionKind.Here, 0);
            return true;
        }

        int digits = start + 2;
        int end = digits;
        while (end < text.Length && end - digits <= MaxIdDigits && char.IsAsciiDigit(text[end]))
            end++;

        if (end == digits || end >= text.Length || text[end] != '>' || text[digits] == '0')
            return false;

        if (!int.TryParse(text.AsSpan(digits, end - digits), out int userId) || userId <= 0)
            return false;

        token = new ChatMentionToken(start, end + 1 - start, ChatMentionKind.User, userId);
        return true;
    }
}
