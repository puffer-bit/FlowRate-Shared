using Shared.Enums;

namespace Shared.Models.Responses.SessionActions.Generic;

public record DeleteSessionResponse(
    Guid RequestId,
    DeleteSessionResult Result ) : GenericSessionResponse(RequestId);
