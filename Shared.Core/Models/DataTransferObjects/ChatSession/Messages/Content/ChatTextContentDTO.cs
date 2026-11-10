using Shared.Models.DataTransferObjects.ChatSession.Messages.Commands;
using System.ComponentModel.DataAnnotations;
using Shared.Validation;
namespace Shared.Models.DataTransferObjects.ChatSession.Messages.Content;

public class ChatTextContentDTO
{
    [Required, NotBlank, StringLength(Limits.ChatTextMaxLength), NoControlCharacters(allowLineBreaks: true)]
    public required string Text { get; init; }

    public ChatCommandResult? CommandResult { get; init; }

    public ChatContentType Type => ChatContentType.Text;
}
