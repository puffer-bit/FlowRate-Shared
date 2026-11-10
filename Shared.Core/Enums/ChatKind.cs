namespace Shared.Enums;

/// <summary>The audience and lifecycle of a text chat, independent of its message transport.</summary>
public enum ChatKind
{
    ServerChannel = 0,
    Direct = 1,
    Group = 2
}
