using Shared.Enums;

namespace Shared.Models.DataTransferObjects;

/// <summary>A coordinator role. Higher <see cref="Position"/> ranks higher; the everyone role is at 0.</summary>
public class RoleDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    /// <summary>0xRRGGBB, or null for no color.</summary>
    public int? Color { get; set; }

    public int Position { get; set; }
    public Permission Permissions { get; set; }

    /// <summary>The role every user has. It cannot be renamed, moved, deleted or assigned.</summary>
    public bool IsEveryone { get; set; }

    /// <summary>Grows with every saved change; a change is accepted only against the current one.</summary>
    public long Revision { get; set; }
}
