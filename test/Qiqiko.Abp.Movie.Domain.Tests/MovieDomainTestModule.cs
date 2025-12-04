using Volo.Abp.Modularity;

namespace Qiqiko.Abp.Movie;

[DependsOn(
    typeof(MovieDomainModule),
    typeof(MovieTestBaseModule)
)]
public class MovieDomainTestModule : AbpModule
{

}
