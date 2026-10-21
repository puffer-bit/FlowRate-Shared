namespace Shared.Models.DataTransferObjects.ChatSession.User;

public class TextChatUserDTO
{
    public required int UserId { get; init; }
    public bool IsAdmin { get; set; }
}
