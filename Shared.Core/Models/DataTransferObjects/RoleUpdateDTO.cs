using System.ComponentModel.DataAnnotations;
using Shared.Enums;
using Shared.Validation;

namespace Shared.Models.DataTransferObjects;

/// <summary>The editable values of a role. The name of the everyone role is kept whatever is sent.</summary>
public class RoleUpdateDTO
{
    [Required, NotBlank, StringLength(Limits.RoleNameMaxLength), NoControlCharacters]
    public string Name { get; set; } = string.Empty;

    [Range(0, Limits.RoleColorMax)]
    public int? Color { get; set; }

    public Permission Permissions { get; set; }
}
