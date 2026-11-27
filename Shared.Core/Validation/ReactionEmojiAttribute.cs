using System.ComponentModel.DataAnnotations;
using Shared.Models.DataTransferObjects.ChatSession.Messages.Reactions;

namespace Shared.Validation;

/// <summary>A single Unicode emoji sequence, including flags, skin tones and joined emoji.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class ReactionEmojiAttribute : ValidationAttribute
{
    public override bool IsValid(object? value) =>
        value is not string text || ChatReactionEmoji.IsSupported(text);

    public override string FormatErrorMessage(string name) => $"{name} must be a single Unicode emoji.";
}
