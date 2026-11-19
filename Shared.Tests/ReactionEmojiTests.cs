using System;
using System.Text.Json;
using Shared.Models;
using Shared.Models.DataTransferObjects.ChatSession.Messages.Reactions;
using Shared.Models.Requests.SessionActions.ChatSession;
using Shared.Serialization;
using Shared.Validation;
using Xunit;

namespace Shared.Tests;

public sealed class ReactionEmojiTests
{
    [Fact]
    public void DefaultQuickChoices_RemainSupported()
    {
        Assert.Equal(8, ChatReactionEmoji.All.Count);
        Assert.All(ChatReactionEmoji.All, emoji => Assert.True(ChatReactionEmoji.IsSupported(emoji)));
    }

    [Theory]
    [InlineData("🦊")]
    [InlineData("🫩")]
    [InlineData("👍🏽")]
    [InlineData("👨‍👩‍👧‍👦")]
    [InlineData("🏴\U000E0067\U000E0062\U000E0065\U000E006E\U000E0067\U000E007F")]
    [InlineData("🇷🇺")]
    [InlineData("1️⃣")]
    [InlineData("❤️‍🔥")]
    [InlineData("❤")]
    public void UnicodeEmoji_AreAcceptedAndPreserved(string emoji)
    {
        BaseMessage request = new SetMessageReactionRequest(1, 2, emoji, true);
        Assert.True(ChatReactionEmoji.IsSupported(emoji));
        Assert.True(MessageValidator.TryValidate(request, out _));

        var decoded = Assert.IsType<SetMessageReactionRequest>(JsonSerializer.Deserialize(
            JsonSerializer.Serialize(request, ProtocolJsonContext.Default.BaseMessage),
            ProtocolJsonContext.Default.BaseMessage));
        Assert.Equal(emoji, decoded.Emoji);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("text")]
    [InlineData("👍👍")]
    [InlineData("🦊 text")]
    [InlineData("🦊\n")]
    [InlineData("\uFE0F")]
    [InlineData("\u200D")]
    [InlineData("🏽")]
    [InlineData("a\uFE0F")]
    [InlineData("1")]
    public void InvalidEmoji_AreRejected(string? emoji)
    {
        Assert.False(ChatReactionEmoji.IsSupported(emoji));
        Assert.False(MessageValidator.TryValidate(new SetMessageReactionRequest(1, 2, emoji!, true), out _));
    }

    [Fact]
    public void OversizedInput_IsRejected() =>
        Assert.False(ChatReactionEmoji.IsSupported(new string('a', ChatReactionEmoji.MaxLength + 1)));
}
