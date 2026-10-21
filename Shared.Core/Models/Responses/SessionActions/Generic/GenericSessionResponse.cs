namespace Shared.Models.Responses.SessionActions.Generic;

public abstract record GenericSessionResponse(Guid RequestId) : Response(RequestId);
