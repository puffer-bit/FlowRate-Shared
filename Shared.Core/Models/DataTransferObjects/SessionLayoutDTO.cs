namespace Shared.Models.DataTransferObjects;

/// <summary>
/// The order of the session list, the same for every user: the sessions outside any category come
/// first, then the categories, each with its own sessions. A session missing from the layout goes
/// to the end of the sessions outside any category.
/// </summary>
public class SessionLayoutDTO
{
    /// <summary>Grows with every change; a client drops a layout older than the one it shows.</summary>
    public required long Revision { get; set; }
    public required List<int> RootSessionIds { get; set; }
    public required List<SessionCategoryDTO> Categories { get; set; }
}

public class SessionCategoryDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required List<int> SessionIds { get; set; }
}
