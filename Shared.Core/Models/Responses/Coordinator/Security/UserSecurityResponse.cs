using Shared.Models.DataTransferObjects;

namespace Shared.Models.Responses.Coordinator.Security;

/// <param name="Security">What the sender logs in with now; null unless the result is NoError.</param>
public record UserSecurityResponse(
    Guid RequestId,
    UserSecurityDTO Security) : CoordinatorResponse(RequestId);
