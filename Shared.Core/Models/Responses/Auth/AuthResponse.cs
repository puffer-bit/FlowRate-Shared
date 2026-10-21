using Shared.Enums.Auth;

namespace Shared.Models.Responses.Auth;

public abstract record AuthResponse(Guid RequestId) : Response(RequestId);
