using System;
using System.Text.Json;
using Shared.Models;
using Shared.Models.DataTransferObjects.ChatSession.Messages;
using Shared.Models.DataTransferObjects.ChatSession.Messages.Content;
using Shared.Models.Notifications.SessionInfo;
using Shared.Models.Requests.SessionActions.ChatSession;
using Shared.Models.Requests.SessionActions.ChatSession.Models;
using Shared.Serialization;
using Xunit;

namespace Shared.Tests;

/// <summary>Attachments are one list of files, each of its kind; an unknown kind is read as a plain file.</summary>
public sealed class AttachmentProtocolTests
{
    private static ChatMessageDTO Message(params ChatFileDTO[] files) => new()
    {
        Id = 1, ChatId = 2, SenderUserId = 3, SentAt = DateTimeOffset.FromUnixTimeMilliseconds(1_795_000_000_000),
        TextContent = null!, ReplyContent = null!, Files = [..files], Reactions = []
    };

    [Fact]
    public void Kinds_RoundTripInOrder()
    {
        BaseMessage sent = new ChatSessionMessageAddedNotification(2, Message(
            new ChatImageFileDTO { Id = 1, FileName = "cat.jpg", Size = 10, MimeType = "image/jpeg", Width = 8, Height = 6 },
            new ChatVideoFileDTO { Id = 1, FileName = "clip.mp4", Size = 20, MimeType = "video/mp4", Duration = TimeSpan.FromSeconds(3) },
            new ChatFileDTO { Id = 2, FileName = "doc.pdf", Size = 30, MimeType = "application/pdf" }));
        var json = JsonSerializer.Serialize(sent, ProtocolJsonContext.Default.BaseMessage);
        var read = Assert.IsType<ChatSessionMessageAddedNotification>(JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage));

        Assert.Collection(read.Message.Files,
            file => Assert.Equal(8, Assert.IsType<ChatImageFileDTO>(file).Width),
            file => Assert.Equal(TimeSpan.FromSeconds(3), Assert.IsType<ChatVideoFileDTO>(file).Duration),
            file => Assert.Equal("doc.pdf", Assert.IsType<ChatFileDTO>(file).FileName));
    }

    [Fact]
    public void UnknownKind_IsReadAsAPlainFile_AndTheMessageSurvives()
    {
        BaseMessage sent = new ChatSessionMessageAddedNotification(2, Message(
            new ChatFileDTO { Id = 5, FileName = "voice.ogg", Size = 40, MimeType = "audio/ogg" }));
        var json = JsonSerializer.Serialize(sent, ProtocolJsonContext.Default.BaseMessage)
            .Replace("\"$type\":\"File\"", "\"$type\":\"Audio\",\"Waveform\":[1,2,3]");
        Assert.Contains("\"Audio\"", json);

        var read = Assert.IsType<ChatSessionMessageAddedNotification>(JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage));
        var file = Assert.Single(read.Message.Files);
        Assert.Equal(typeof(ChatFileDTO), file.GetType());
        Assert.Equal("voice.ogg", file.FileName);
    }

    [Fact]
    public void UnknownNewFileKind_IsTakenAsAPlainFile()
    {
        var json = """
            {"$type":"SendMessageRequest","RequestId":"6f1c2c1e-6a43-4c43-9a3c-1b8c0f0e7d11","ChatId":1,
             "Message":{"ChatId":1,"Files":[{"$type":"Audio","UploadId":"00","FileName":"voice.ogg"}]}}
            """;
        var read = Assert.IsType<SendMessageRequest>(JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage));
        Assert.Equal(typeof(NewChatFileModel), Assert.Single(read.Message.Files).GetType());
    }
}
