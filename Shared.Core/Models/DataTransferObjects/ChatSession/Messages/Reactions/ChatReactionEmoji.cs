namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Reactions;

/// <summary>The reaction palette accepted by the server and offered by the clients.</summary>
public static class ChatReactionEmoji
{
    public static IReadOnlyList<string> All { get; } = Array.AsReadOnly(new[] { "👍", "👎", "❤️", "😂", "😮", "😢", "🎉", "🔥" });

    public static bool IsSupported(string emoji) => All.Contains(emoji, StringComparer.Ordinal);
}
