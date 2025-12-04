using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Qiqiko.Abp.Movie;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class MovieInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MovieInstallerModule>();
        });
    }
}
