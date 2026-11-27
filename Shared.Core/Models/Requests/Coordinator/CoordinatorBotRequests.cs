using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Responses.Coordinator;
using Shared.Validation;

namespace Shared.Models.Requests.Coordinator;

/// <summary>The bots of the coordinator, for users with <see cref="Permission.ManageBots"/>.</summary>
public record CoordinatorBotsRequest : CoordinatorRequest
{
    public CoordinatorBotsResponse ToResponse(List<UserDTO> bots) => new(RequestId, bots);
}

/// <summary>
/// Makes a bot owned by the sender, with the everyone role only, and a login code for it. Refused with
/// <see cref="ErrorCode.NameTaken"/> for a taken username and <see cref="ErrorCode.BotLimitReached"/> past
/// the limit of the coordinator. With <paramref name="IsDebug"/> the bot is a <see cref="Enums.User.UserKind.DebugBot"/>,
/// which the limit does not count.
/// </summary>
public record CoordinatorCreateBotRequest(
    [property: Required, NotBlank, StringLength(Limits.UsernameMaxLength), NoControlCharacters] string Username,
    bool IsDebug = false)
    : CoordinatorRequest
{
    public CoordinatorCreateBotResponse ToResponse(UserDTO bot, BotLoginCodeDTO loginCode) =>
        new(RequestId, bot, loginCode);
}

/// <summary>
/// A new login code for a bot, for a bot that lost its key or moves to another machine. Codes given before stop
/// working; with <paramref name="EndDevices"/> the devices of the bot are ended too, which disconnects it.
/// </summary>
public record CoordinatorBotLoginCodeRequest([property: EntityId] int TargetUserId, bool EndDevices = true)
    : CoordinatorRequest
{
    public CoordinatorBotLoginCodeResponse ToResponse(BotLoginCodeDTO loginCode) =>
        new(RequestId, TargetUserId, loginCode);
}

/// <summary>Deletes a bot as <see cref="CoordinatorDeleteUserRequest"/> deletes a user; only bots are taken.</summary>
public record CoordinatorDeleteBotRequest([property: EntityId] int TargetUserId) : CoordinatorRequest
{
    public CoordinatorDeleteBotResponse ToResponse() => new(RequestId, TargetUserId);
}
