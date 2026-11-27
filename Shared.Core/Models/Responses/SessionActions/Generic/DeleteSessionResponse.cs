namespace Shared.Models.Responses.SessionActions.Generic;

public record DeleteSessionResponse(Guid RequestId) : GenericSessionResponse(RequestId);
