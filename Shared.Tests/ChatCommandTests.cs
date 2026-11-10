using Shared.Models.DataTransferObjects.ChatSession.Messages.Commands;
using Xunit;

namespace Shared.Tests;

public class ChatCommandTests
{
    [Theory]
    [InlineData("/roll", 1, 100)]
    [InlineData("/ROLL 1-1000", 1, 1000)]
    [InlineData("/roll 7-7", 7, 7)]
    [InlineData("/roll 5 - 20", 5, 20)]
    [InlineData("/flip", 0, 1)]
    public void ParsesInclusiveBounds(string text, int minimum, int maximum)
    {
        var command = ChatCommandRequest.Parse(text);
        Assert.NotNull(command);
        Assert.True(command.IsValid);
        Assert.Equal(minimum, command.Minimum);
        Assert.Equal(maximum, command.Maximum);
    }

    [Theory]
    [InlineData("/roll 0-100")]
    [InlineData("/roll 1-1001")]
    [InlineData("/roll 100-1")]
    [InlineData("/roll -1-100")]
    [InlineData("/roll 1.5-10")]
    [InlineData("/roll 999999999999-100")]
    [InlineData("/roll 10")]
    [InlineData("/roll 1-10 extra")]
    [InlineData("/roll\n1-10")]
    [InlineData("/flip heads")]
    public void RecognizesInvalidArguments(string text) => Assert.False(ChatCommandRequest.Parse(text)!.IsValid);

    [Theory]
    [InlineData("hello /roll")]
    [InlineData("/unknown")]
    [InlineData("/roller")]
    [InlineData(null)]
    public void LeavesOrdinaryTextAlone(string? text) => Assert.Null(ChatCommandRequest.Parse(text));
}
