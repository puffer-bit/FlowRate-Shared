using System.Collections.Frozen;

namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Reactions;

/// <summary>The default reaction palette and the Unicode emoji accepted as reactions.</summary>
public static class ChatReactionEmoji
{
    public const int MaxLength = 32;

    public static IReadOnlyList<string> All { get; } = Array.AsReadOnly(new[] { "👍", "👎", "❤️", "😂", "😮", "😢", "🎉", "🔥" });

    private static readonly Lazy<FrozenSet<string>> Supported = new(Load);

    public static bool IsSupported(string? emoji) =>
        emoji is { Length: > 0 and <= MaxLength } && Supported.Value.Contains(emoji);

    private static FrozenSet<string> Load()
    {
        using var stream = typeof(ChatReactionEmoji).Assembly.GetManifestResourceStream("Shared.ReactionEmoji.txt")!;
        using var reader = new StreamReader(stream);
        List<string> emoji = [];
        while (reader.ReadLine() is { } line)
            emoji.Add(line);
        return emoji.ToFrozenSet(StringComparer.Ordinal);
    }
}
