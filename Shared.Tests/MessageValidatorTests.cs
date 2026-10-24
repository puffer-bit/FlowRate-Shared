using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Shared.Enums;
using Shared.Enums.WebRTC;
using Shared.Models;
using Shared.Models.DataTransferObjects;
using Shared.Models.DataTransferObjects.ChatSession.Messages.Content;
using Shared.Models.Media.Transport;
using Shared.Models.Requests;
using Shared.Models.Requests.Auth;
using Shared.Models.Requests.Coordinator;
using Shared.Models.Requests.SessionActions.ChatSession;
using Shared.Models.Requests.SessionActions.ChatSession.Models;
using Shared.Models.Requests.SessionActions.Generic;
using Shared.Models.Requests.SessionActions.Generic.Models;
using Shared.Models.Requests.SessionActions.Generic.Models.SessionData;
using Shared.Models.Requests.SessionActions.HybridSession;
using Shared.Validation;
using Xunit;

namespace Shared.Tests;

public class MessageValidatorTests
{
    // A video cannot be attached so far, and a request with one is rejected as a whole, so its
    // fields get limits once it can be uploaded
    private static readonly HashSet<Type> PendingMediaTypes =
    [
        typeof(ChatVideoContentDTO)
    ];

    private static readonly Assembly SharedAssembly = typeof(BaseMessage).Assembly;

    public static TheoryData<Type> ConcreteRequestTypes()
    {
        var data = new TheoryData<Type>();
        foreach (var type in SharedAssembly.GetTypes()
                     .Where(t => typeof(Request).IsAssignableFrom(t) && !t.IsAbstract)
                     .OrderBy(t => t.FullName))
            data.Add(type);
        return data;
    }

    [Theory]
    [MemberData(nameof(ConcreteRequestTypes))]
    public void EveryStringAndCollection_InARequest_HasASizeLimit(Type requestType)
    {
        var unbounded = new List<string>();
        var visited = new HashSet<Type>();
        CollectUnbounded(requestType, requestType.Name, unbounded, visited);

        Assert.True(unbounded.Count == 0, "No size limit on: " + string.Join(", ", unbounded));
    }

    [Theory]
    [MemberData(nameof(ConcreteRequestTypes))]
    public void Validator_HandlesEmptyRequestsWithoutThrowing(Type requestType)
    {
        var request = RuntimeHelpers.GetUninitializedObject(requestType);

        MessageValidator.TryValidate(request, out _);
    }

    [Fact]
    public void TypicalRequests_AreValid()
    {
        object[] requests =
        [
            new UserAuthRequest("secret", "alice"),
            new CoordinatorUserInfoRequest([1, 2, 3]),
            new CoordinatorChangeUsernameRequest("alice"),
            new CoordinatorSetAvatarRequest(new string('a', Limits.MediaUploadIdLength * 2)),
            new CoordinatorSetAvatarRequest(null, 7),
            new RegisterPushTokenRequest("token", PushPlatform.Android, new string('a', Limits.PushKeyIdLength * 2),
                Convert.ToBase64String(new byte[Limits.PushKeyLength])),
            new UnregisterPushTokenRequest("token"),
            new CreateSessionRequest(new CreateSessionModel("Room", 10, new HybridSessionCreateData(WebRTCEngine.Mediasoup))),
            new CreateSessionRequest(new CreateSessionModel("Room", 10, new HybridSessionCreateData(WebRTCEngine.Mediasoup,
                Media: new HybridMediaSettingsDTO { CameraCodec = WebRTCVideoCodecs.H264, IsAudioNackEnabled = true }))),
            new ChangeHybridSessionSettingsRequest(1, "Room", 10, false, new HybridMediaSettingsDTO()),
            new CreateSessionRequest(new CreateSessionModel("Chat", 50, new ChatSessionCreateData("About\nus", "https://example.com/icon.png"))),
            new KickFromSessionRequest(1, 2, SessionType.Hybrid, "spam"),
            new SessionInfoRequest(null),
            new CreateSessionRequest(new CreateSessionModel("Room", 10, new ChatSessionCreateData(), CategoryId: 3)),
            new CreateSessionCategoryRequest("Voice rooms"),
            new RenameSessionCategoryRequest(1, "Games"),
            new DeleteSessionCategoryRequest(1),
            new MoveSessionRequest(1, null, 0),
            new MoveSessionRequest(1, 2, 5),
            new MoveSessionCategoryRequest(1, 0),
            new ChatSessionHistoryRequest(1, 50, 0),
            new ChatSessionHistoryPageRequest(1, null, 50),
            new ChatSessionHistoryPageRequest(1, 7, 50),
            new ChatSessionChangesRequest(1, 0, 50),
            new ChatReadStatesRequest(),
            new ChatMarkReadRequest(1, 2),
            new EditTextMessageRequest(1, 2, "fixed\ttypo"),
            new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, TextContent = new ChatTextContentDTO { Text = "hi" }, ReplyToMessageId = 5 }),
            new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Images = [Image("a", "b")] }),
            new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Files = [File("c", "notes.txt")] }),
            new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Images = [Image("a", "b")], Files = [File("c")] }),
            new MediaTokenRequest(),
            new HybridSessionJoinRequest(1) { ClientEpoch = Guid.NewGuid().ToString("N") },
            ConnectTransport(new DTLSFingerprint { Algorithm = "sha-256", Fingerprint = "AB:CD:EF:01" })
        ];

        foreach (var request in requests)
            Assert.True(MessageValidator.TryValidate(request, out var errors), $"{request.GetType().Name}: {string.Join("; ", errors)}");
    }

    // The ids are padded to the length of an upload id, so a test names them by their first digits
    private static NewChatImageModel Image(string upload, string preview, string? uploadId = null, int width = 640,
        string fileName = "photo.jpg") => new()
    {
        UploadId = uploadId ?? upload.PadRight(Limits.MediaUploadIdLength * 2, '0'),
        PreviewUploadId = preview.PadRight(Limits.MediaUploadIdLength * 2, '0'),
        FileName = fileName,
        Width = width,
        Height = 480
    };

    private static NewChatFileModel File(string upload, string fileName = "notes.txt") => new()
    {
        UploadId = upload.PadRight(Limits.MediaUploadIdLength * 2, '0'),
        FileName = fileName
    };

    public static TheoryData<object, string> InvalidRequests() => new()
    {
        { new UserAuthRequest("secret", new string('a', Limits.UserIdentityMaxLength + 1)), "UserIdentity" },
        { new CoordinatorUserInfoRequest(Enumerable.Range(1, Limits.UserIdsMaxCount + 1).ToList()), "UserIds" },
        { new CoordinatorUserInfoRequest([]), "UserIds" },
        { new CoordinatorChangeUsernameRequest(new string('u', Limits.UsernameMaxLength + 1)), "Username" },
        { new CoordinatorChangeUsernameRequest("   "), "Username" },
        { new CoordinatorChangeUsernameRequest("bad\u0007name"), "Username" },
        { new CoordinatorSetAvatarRequest("xyz"), "UploadId" },
        { new CoordinatorSetAvatarRequest(new string('a', Limits.MediaUploadIdLength * 2 + 2)), "UploadId" },
        { new RegisterPushTokenRequest(new string('t', Limits.PushTokenMaxLength + 1), PushPlatform.Android, new string('a', Limits.PushKeyIdLength * 2), Convert.ToBase64String(new byte[Limits.PushKeyLength])), "Token" },
        { new RegisterPushTokenRequest("token", (PushPlatform)99, new string('a', Limits.PushKeyIdLength * 2), Convert.ToBase64String(new byte[Limits.PushKeyLength])), "Platform" },
        { new RegisterPushTokenRequest("token", PushPlatform.Android, "xyz", Convert.ToBase64String(new byte[Limits.PushKeyLength])), "KeyId" },
        { new RegisterPushTokenRequest("token", PushPlatform.Android, new string('a', Limits.PushKeyIdLength * 2), Convert.ToBase64String(new byte[Limits.PushKeyLength - 1])), "Key" },
        { new RegisterPushTokenRequest("token", PushPlatform.Android, new string('a', Limits.PushKeyIdLength * 2), "not base64!"), "Key" },
        { new UnregisterPushTokenRequest("  "), "Token" },
        { new CreateSessionRequest(new CreateSessionModel(new string('n', Limits.SessionNameMaxLength + 1), 10, new ChatSessionCreateData())), "Name" },
        { new CreateSessionRequest(new CreateSessionModel("   ", 10, new ChatSessionCreateData())), "Name" },
        { new CreateSessionRequest(new CreateSessionModel("bad\u0007name", 10, new ChatSessionCreateData())), "Name" },
        { new CreateSessionRequest(new CreateSessionModel("Room", 0, new ChatSessionCreateData())), "Capacity" },
        { new CreateSessionRequest(new CreateSessionModel("Room", 10, new ChatSessionCreateData(IconPath: "file:///etc/passwd"))), "IconPath" },
        { new CreateSessionRequest(new CreateSessionModel("Room", 10, new HybridSessionCreateData((WebRTCEngine)99))), "EngineType" },
        { new CreateSessionRequest(new CreateSessionModel("Room", 10, new HybridSessionCreateData(WebRTCEngine.Mediasoup, VideoBitrate: 1_000_000))), "VideoBitrate" },
        { new CreateSessionRequest(new CreateSessionModel("Room", 10, new HybridSessionCreateData(WebRTCEngine.Mediasoup, Media: new HybridMediaSettingsDTO { CameraCodec = WebRTCVideoCodecs.AV1 }))), "CameraCodec" },
        { new CreateSessionRequest(new CreateSessionModel("Room", 10, new HybridSessionCreateData(WebRTCEngine.Mediasoup, Media: new HybridMediaSettingsDTO { AudioCodec = WebRTCAudioCodecs.G711 }))), "AudioCodec" },
        { new ChangeHybridSessionSettingsRequest(1, "Room", 10, false, new HybridMediaSettingsDTO { OpusPacketLossPercent = 101 }), "OpusPacketLossPercent" },
        { new ChangeHybridSessionSettingsRequest(1, "Room", 10, false, new HybridMediaSettingsDTO { VoiceJitterBufferMs = 5 }), "VoiceJitterBufferMs" },
        { new ChangeHybridSessionSettingsRequest(1, "Room", 10, false, null!), "Media" },
        { new KickFromSessionRequest(1, 2, SessionType.Hybrid, new string('r', Limits.ReasonMaxLength + 1)), "Reason" },
        { new DeleteSessionRequest(0), "SessionId" },
        { new CreateSessionRequest(new CreateSessionModel("Room", 10, new ChatSessionCreateData(), CategoryId: 0)), "CategoryId" },
        { new CreateSessionCategoryRequest(new string('n', Limits.SessionCategoryNameMaxLength + 1)), "Name" },
        { new CreateSessionCategoryRequest("  "), "Name" },
        { new RenameSessionCategoryRequest(1, "bad\u0007name"), "Name" },
        { new RenameSessionCategoryRequest(0, "Games"), "CategoryId" },
        { new DeleteSessionCategoryRequest(0), "CategoryId" },
        { new MoveSessionRequest(0, null, 0), "SessionId" },
        { new MoveSessionRequest(1, 0, 0), "CategoryId" },
        { new MoveSessionRequest(1, null, -1), "Index" },
        { new MoveSessionRequest(1, null, Limits.SessionLayoutIndexMax + 1), "Index" },
        { new MoveSessionCategoryRequest(1, -1), "Index" },
        { new ChatSessionHistoryRequest(1, Limits.ChatHistoryMaxTake + 1, 0), "Take" },
        { new ChatSessionHistoryPageRequest(1, 0, 50), "BeforeMessageId" },
        { new ChatSessionHistoryPageRequest(1, null, Limits.ChatHistoryMaxTake + 1), "Take" },
        { new ChatSessionChangesRequest(1, -1, 50), "AfterRevision" },
        { new ChatSessionChangesRequest(1, 0, 0), "Take" },
        { new ChatMarkReadRequest(1, 0), "MessageId" },
        { new EditTextMessageRequest(1, 2, new string('t', Limits.ChatTextMaxLength + 1)), "Text" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1 }), "text or an attachment" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, TextContent = new ChatTextContentDTO { Text = "" } }), "Text" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Images = [Image("a", "a")] }), "attached once" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Images = [Image("a", "b"), Image("c", "B")] }), "attached once" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Images = [Image("a", "b", uploadId: "not-hex")] }), "UploadId" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Images = [Image("a", "b", width: 0)] }), "Width" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Images = [Image("a", "b", fileName: " ")] }), "FileName" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Images = Enumerable.Range(0, Limits.ChatAttachmentsMaxCount + 1).Select(i => Image($"a{i:x}", $"b{i:x}")).ToList() }), "Images" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Video = [new ChatVideoContentDTO { Id = 1, FileName = "a.mp4", Size = 1, MimeType = "video/mp4", Width = 1, Height = 1, Duration = TimeSpan.FromSeconds(1) }] }), "not supported" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Images = [Image("a", "b")], Files = [File("B")] }), "attached once" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Files = [File("a"), File("A")] }), "attached once" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Files = [File("a", "bad\u0001name")] }), "FileName" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Files = [new NewChatFileModel { UploadId = "not-hex", FileName = "a.txt" }] }), "UploadId" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, Files = Enumerable.Range(0, Limits.ChatAttachmentsMaxCount + 1).Select(i => File($"a{i:x}")).ToList() }), "Files" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, TextContent = new ChatTextContentDTO { Text = "x" }, IsDelayed = true }), "DelayedSentTime" },
        { new SendTextMessageRequest(1, new NewChatMessageModel { ChatId = 1, TextContent = new ChatTextContentDTO { Text = "x" }, IsDisposable = true, DisposeTime = DateTime.UtcNow.AddYears(5) }), "DisposeTime" },
        { new HybridSessionJoinRequest(1) { ClientEpoch = "not-a-guid" }, "ClientEpoch" },
        { ConnectTransport(new DTLSFingerprint { Algorithm = "md5", Fingerprint = "AB:CD" }), "Fingerprints[0]" },
        { ConnectTransport(new DTLSFingerprint { Algorithm = "sha-256", Fingerprint = "<script>" }), "Fingerprints[0]" },
    };

    [Theory]
    [MemberData(nameof(InvalidRequests))]
    public void InvalidRequests_AreRejected(object request, string expectedInError)
    {
        Assert.False(MessageValidator.TryValidate(request, out var errors));
        Assert.Contains(errors, error => error.Contains(expectedInError, StringComparison.OrdinalIgnoreCase));
    }

    private static HybridSessionConnectTransportRequest ConnectTransport(DTLSFingerprint fingerprint) =>
        new(1) { DtlsParameters = new DTLSParameters { Role = "client", Fingerprints = [fingerprint] } };

    private static void CollectUnbounded(Type type, string path, List<string> unbounded, HashSet<Type> visited)
    {
        if (!visited.Add(type) || PendingMediaTypes.Contains(type))
            return;

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            var propertyPath = $"{path}.{property.Name}";
            var bounded = property.GetCustomAttributes<ValidationAttribute>()
                .Any(a => a is StringLengthAttribute or ItemCountAttribute or OneOfAttribute);

            if (propertyType == typeof(string))
            {
                if (!bounded)
                    unbounded.Add(propertyPath);
                continue;
            }

            if (typeof(IEnumerable).IsAssignableFrom(propertyType))
            {
                if (!bounded)
                    unbounded.Add(propertyPath);

                var elementType = propertyType.IsArray
                    ? propertyType.GetElementType()
                    : propertyType.GetGenericArguments().FirstOrDefault();
                if (elementType != null)
                    CollectNested(elementType, propertyPath + "[]", unbounded, visited);
                continue;
            }

            CollectNested(propertyType, propertyPath, unbounded, visited);
        }
    }

    private static void CollectNested(Type type, string path, List<string> unbounded, HashSet<Type> visited)
    {
        if (type.Assembly != SharedAssembly || type.IsEnum)
            return;

        // A polymorphic property may hold any derived type, so each of them is checked
        foreach (var candidate in SharedAssembly.GetTypes().Where(t => type.IsAssignableFrom(t) && !t.IsAbstract))
            CollectUnbounded(candidate, path, unbounded, visited);
    }
}
