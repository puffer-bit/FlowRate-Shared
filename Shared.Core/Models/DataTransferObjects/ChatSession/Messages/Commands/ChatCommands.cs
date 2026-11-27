using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Shared.Validation;

namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Commands;

public enum ChatCommandKind { Roll = 1, Flip = 2 }

/// <summary>The server's stored outcome. Clients never choose the result.</summary>
public sealed record ChatCommandResult
{
    [DefinedEnum]
    public required ChatCommandKind Kind { get; init; }
    [Range(0, 1000)]
    public required int Minimum { get; init; }
    [Range(1, 1000)]
    public required int Maximum { get; init; }
    [Range(0, 1000)]
    public required int Value { get; init; }

    public bool IsValid() => Kind switch
    {
        ChatCommandKind.Roll => Minimum >= 1 && Minimum <= Maximum && Maximum <= 1000
            && Value >= Minimum && Value <= Maximum,
        ChatCommandKind.Flip => Minimum == 0 && Maximum == 1 && Value is 0 or 1,
        _ => false
    };

    public string ToFallbackText() => Kind == ChatCommandKind.Roll
        ? FormattableString.Invariant($"/roll {Minimum}-{Maximum} → {Value}")
        : "/flip → " + (Value == 0 ? "heads" : "tails");
}

/// <summary>A recognized command, including invalid arguments so callers can show a useful error.</summary>
public sealed record ChatCommandRequest(ChatCommandKind Kind, int Minimum, int Maximum, bool IsValid)
{
    public static ChatCommandRequest? Parse(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;
        text = text.Trim();
        int separator = 0;
        while (separator < text.Length && !char.IsWhiteSpace(text[separator])) separator++;
        string name = text[..separator];
        ChatCommandKind kind;
        if (name.Equals("/roll", StringComparison.OrdinalIgnoreCase)) kind = ChatCommandKind.Roll;
        else if (name.Equals("/flip", StringComparison.OrdinalIgnoreCase)) kind = ChatCommandKind.Flip;
        else return null;
        string arguments = text[separator..].Trim();
        var invalid = new ChatCommandRequest(kind, 0, 0, false);
        if (text.AsSpan().ContainsAny('\r', '\n')) return invalid;
        if (kind == ChatCommandKind.Flip)
            return arguments.Length == 0 ? new(kind, 0, 1, true) : invalid;
        if (arguments.Length == 0) return new(kind, 1, 100, true);
        var bounds = arguments.Split('-', StringSplitOptions.TrimEntries);
        if (bounds.Length != 2
            || !int.TryParse(bounds[0], NumberStyles.None, CultureInfo.InvariantCulture, out int minimum)
            || !int.TryParse(bounds[1], NumberStyles.None, CultureInfo.InvariantCulture, out int maximum)
            || minimum < 1 || minimum > maximum || maximum > 1000)
            return invalid;
        return new(kind, minimum, maximum, true);
    }
}
