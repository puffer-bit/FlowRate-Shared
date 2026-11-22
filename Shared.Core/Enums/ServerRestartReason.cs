namespace Shared.Enums;

/// <summary>Why the server goes down.</summary>
public enum ServerRestartReason
{
    /// <summary>It comes back by itself in a moment; also what a plain stop of the process tells.</summary>
    Restart = 0,
    /// <summary>It comes back with a new version.</summary>
    Update = 1,
    /// <summary>It is turned off and is not expected back soon; clients do not reconnect on their own.</summary>
    Shutdown = 2
}
