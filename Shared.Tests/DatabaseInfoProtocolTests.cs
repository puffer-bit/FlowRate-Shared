using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Shared.Models;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Serialization;
using Xunit;

namespace Shared.Tests;

public sealed class DatabaseInfoProtocolTests
{
    [Theory]
    [InlineData("SQLite")]
    [InlineData("PostgreSQL")]
    [InlineData(null)]
    public void DatabaseProvider_RoundTrips(string? provider)
    {
        BaseMessage response = new CoordinatorSettingsResponse(Guid.NewGuid(), new CoordinatorSettingsDTO(), [], provider);
        var decoded = Decode(JsonSerializer.Serialize(response, ProtocolJsonContext.Default.BaseMessage));
        Assert.Equal(provider, decoded.DatabaseProvider);
    }

    [Fact]
    public void EarlierSettingsResponse_WithoutDatabaseProvider_IsReadable()
    {
        BaseMessage response = new CoordinatorSettingsResponse(Guid.NewGuid(), new CoordinatorSettingsDTO(), []);
        var json = JsonNode.Parse(JsonSerializer.Serialize(response, ProtocolJsonContext.Default.BaseMessage))!;
        json.AsObject().Remove("DatabaseProvider");
        Assert.Null(Decode(json.ToJsonString()).DatabaseProvider);
    }

    private static CoordinatorSettingsResponse Decode(string json) =>
        Assert.IsType<CoordinatorSettingsResponse>(JsonSerializer.Deserialize(json, ProtocolJsonContext.Default.BaseMessage));
}
