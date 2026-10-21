using Shared.Enums;
using Shared.Models.Media.Transport;

namespace Shared.Models.Responses.SessionActions.Generic;

public record JoinSessionResponse(
    Guid RequestId,
    int SessionId,
    SessionType SessionType,
    JoinSessionResult Result,
    TransportData? Data) : GenericSessionResponse(RequestId);
