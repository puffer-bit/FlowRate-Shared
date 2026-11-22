using System;
using System.Text.Json;
using Shared.Models;
using Shared.Enums.User;
using Shared.Models.DataTransferObjects;
using Shared.Models.Notifications.SessionInfo;
using Shared.Models.Responses.Coordinator;
using Shared.Serialization;
using Xunit;

namespace Shared.Tests;

/// <summary>Times travel as Unix milliseconds and durations as milliseconds, never as strings.</summary>
public sealed class UnixTimeConverterTests
{
    private static readonly DateTimeOffset Moment = DateTimeOffset.FromUnixTimeMilliseconds(1_795_000_000_123);

    [Fact]
    public void Time_IsWrittenAsUnixMilliseconds_AndReadBack()
    {
        BaseMessage message = new ChatPeerReadNotification(1, 2, 3, Moment);
        var json = JsonSerializer.Serialize(message, ProtocolJsonContext.Default.BaseMessage);
        Assert.Contains("\"ReadAt\":1795000000123", json);
        var read = Assert.IsType<ChatPeerReadNotification>(JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage));
        Assert.Equal(Moment, read.ReadAt);
        Assert.Equal(TimeSpan.Zero, read.ReadAt.Offset);
    }

    [Fact]
    public void LocalOffset_IsTheSameMomentOnTheWire()
    {
        var local = Moment.ToOffset(TimeSpan.FromHours(3));
        BaseMessage message = new ChatPeerReadNotification(1, 2, 3, local);
        Assert.Contains("\"ReadAt\":1795000000123", JsonSerializer.Serialize(message, ProtocolJsonContext.Default.BaseMessage));
    }

    [Fact]
    public void NullableTime_IsNullOrANumber()
    {
        var ban = new BanDTO(Guid.NewGuid(), BanKind.Account, 1, null, "", 2, Moment, null);
        var json = JsonSerializer.Serialize(new CoordinatorBansResponse(Guid.NewGuid(), [ban]) as BaseMessage,
            ProtocolJsonContext.Default.BaseMessage);
        Assert.Contains("\"CreatedAt\":1795000000123", json);
        Assert.Contains("\"ExpiresAt\":null", json);
    }

    [Theory]
    [InlineData("\"2026-11-22T10:00:00Z\"")]
    [InlineData("1.5")]
    [InlineData("99999999999999999")]
    public void ReadAt_RejectsStringsFractionsAndOutOfRange(string value)
    {
        var json = $"{{\"$type\":\"ChatPeerReadNotification\",\"ChatId\":1,\"ReaderUserId\":2,\"LastReadMessageId\":3,\"ReadAt\":{value}}}";
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage));
    }

    [Fact]
    public void Duration_IsWrittenAsMilliseconds()
    {
        var options = new JsonSerializerOptions { Converters = { new MillisecondsConverter() } };
        Assert.Equal("83456", JsonSerializer.Serialize(TimeSpan.FromMilliseconds(83_456), options));
        Assert.Equal(TimeSpan.FromMilliseconds(83_456), JsonSerializer.Deserialize<TimeSpan>("83456", options));
    }
}
