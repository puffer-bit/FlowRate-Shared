namespace Shared.Models.Responses.Coordinator;

public abstract record CoordinatorResponse(Guid RequestId) : Response(RequestId);
