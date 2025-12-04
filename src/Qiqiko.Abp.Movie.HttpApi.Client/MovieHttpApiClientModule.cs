using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Qiqiko.Abp.Movie;

[DependsOn(
    typeof(MovieApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class MovieHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(MovieApplicationContractsModule).Assembly,
            MovieRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MovieHttpApiClientModule>();
        });

    }
}
