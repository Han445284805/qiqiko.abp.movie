using Qiqiko.Abp.Movie.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Qiqiko.Abp.Movie.Permissions;

public class MoviePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MoviePermissions.GroupName, L("Permission:Movie"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MovieResource>(name);
    }
}
