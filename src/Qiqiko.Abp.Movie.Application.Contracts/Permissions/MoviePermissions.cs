using Volo.Abp.Reflection;

namespace Qiqiko.Abp.Movie.Permissions;

public class MoviePermissions
{
    public const string GroupName = "Movie";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MoviePermissions));
    }
}
