using System.Collections.Generic;
using Shared.Enums;
using Shared.Models.DataTransferObjects;
using Shared.Models.Requests.SessionActions.Generic;
using Shared.Permissions;
using Shared.Validation;
using Xunit;

namespace Shared.Tests;

public class SessionPermissionsTests
{
    private const int Everyone = 1;
    private const int Member = 2;
    private const int Moderator = 3;

    private static Permission Resolve(Permission granted, int[] roleIds, params PermissionOverride[] overrides) =>
        SessionPermissions.Resolve(granted, overrides, Everyone, roleIds);

    [Fact]
    public void WithoutOverrides_TheCoordinatorPermissionsHold() =>
        Assert.Equal(Permission.Default, Resolve(Permission.Default, [Member]));

    [Fact]
    public void EveryoneDeny_HidesTheSession() =>
        Assert.False(Resolve(Permission.Default, [], new PermissionOverride(Everyone, Permission.None, Permission.ViewSessions))
            .HasFlag(Permission.ViewSessions));

    [Fact]
    public void RoleAllow_BeatsEveryoneDeny()
    {
        var permissions = Resolve(Permission.Default, [Member],
            new PermissionOverride(Everyone, Permission.None, Permission.ViewSessions),
            new PermissionOverride(Member, Permission.ViewSessions, Permission.None));

        Assert.True(permissions.HasFlag(Permission.ViewSessions));
    }

    [Fact]
    public void BetweenRoles_AllowBeatsDeny()
    {
        var permissions = Resolve(Permission.Default, [Member, Moderator],
            new PermissionOverride(Member, Permission.None, Permission.SendMessages),
            new PermissionOverride(Moderator, Permission.SendMessages, Permission.None));

        Assert.True(permissions.HasFlag(Permission.SendMessages));
    }

    [Fact]
    public void OverridesOfOtherRoles_DoNotApply() =>
        Assert.Equal(Permission.Default,
            Resolve(Permission.Default, [Member], new PermissionOverride(Moderator, Permission.None, Permission.SendMessages)));

    [Fact]
    public void Administrator_IsNeverLimited() =>
        Assert.Equal(Permission.All,
            Resolve(Permission.Administrator, [Member], new PermissionOverride(Member, Permission.None, Permission.ViewSessions)));

    [Fact]
    public void CoordinatorWidePermissions_CannotBeOverridden() =>
        Assert.False(Resolve(Permission.Default, [Member], new PermissionOverride(Member, Permission.ManageRoles, Permission.None))
            .HasFlag(Permission.ManageRoles));

    [Theory]
    [InlineData(Permission.SendMessages, Permission.SendMessages, false)]
    [InlineData(Permission.ManageRoles, Permission.None, false)]
    [InlineData(Permission.SendMessages, Permission.ViewSessions, true)]
    public void Override_IsValidated(Permission allow, Permission deny, bool valid)
    {
        var request = new ChangeSessionPermissionsRequest(1,
            [new SessionPermissionOverrideDTO { RoleId = Everyone, Allow = allow, Deny = deny }]);

        Assert.Equal(valid, MessageValidator.TryValidate(request, out _));
    }
}
