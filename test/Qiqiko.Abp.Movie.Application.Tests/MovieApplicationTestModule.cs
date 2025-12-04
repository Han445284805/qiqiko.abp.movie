using Volo.Abp.Modularity;

namespace Qiqiko.Abp.Movie;

[DependsOn(
    typeof(MovieApplicationModule),
    typeof(MovieDomainTestModule)
    )]
public class MovieApplicationTestModule : AbpModule
{

}
