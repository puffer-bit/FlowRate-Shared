using System;
using System.Text.Json;
using Shared.Models;
using Shared.Models.DataTransferObjects.ChatSession.Messages.Content;
using Shared.Models.Requests.SessionActions.ChatSession;
using Shared.Models.Responses.SessionActions.ChatSession;
using Shared.Serialization;
using Shared.Validation;
using Xunit;

namespace Shared.Tests;

public sealed class LinkPreviewProtocolTests
{
    [Fact]
    public void AnEditWithoutTheFlag_LeavesThePreviewAsItWas()
    {
        const string json = """{"$type":"EditMessageRequest","ChatId":1,"MessageId":2,"Text":"hi","RequestId":"bc52bace-965d-4ee8-afa6-0b7164708bb6","SenderId":0}""";

        var request = Assert.IsType<EditMessageRequest>(
            JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage));

        Assert.Null(request.IsLinkPreviewDisabled);
    }

    [Fact]
    public void APreviewResponse_RoundTrips()
    {
        var preview = new ChatLinkPreviewDTO
        {
            Id = 5, Url = "https://example.com", SiteName = "Example", Title = "Title", Description = "Text",
            HasImage = true, ImageWidth = 1200, ImageHeight = 630, IsLargeImage = true
        };
        BaseMessage response = new ChatLinkPreviewResponse(Guid.NewGuid(), preview);

        var decoded = Assert.IsType<ChatLinkPreviewResponse>(JsonSerializer.Deserialize(
            JsonSerializer.Serialize(response, ProtocolJsonContext.Default.BaseMessage),
            ProtocolJsonContext.Default.BaseMessage));

        Assert.Equal("Title", decoded.Preview?.Title);
        Assert.True(decoded.Preview?.IsLargeImage);
    }

    [Theory]
    [InlineData("https://example.com", true)]
    [InlineData("", false)]
    [InlineData("https://example.com/\n", false)]
    public void APreviewRequest_IsValidated(string url, bool valid) =>
        Assert.Equal(valid, MessageValidator.TryValidate(new ChatLinkPreviewRequest(1, url), out _));
}
