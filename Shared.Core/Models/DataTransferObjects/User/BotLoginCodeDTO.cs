namespace Shared.Models.DataTransferObjects;

/// <summary>
/// A one-time code a bot logs in with once, as <c>LoginCodeLoginDTO</c>: it adds the device key of the bot, which logs
/// in from then on. Shown once; the server keeps only its hash.
/// </summary>
public record BotLoginCodeDTO(string Code, DateTimeOffset ExpiresAt);
