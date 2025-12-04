using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Qiqiko.Abp.Movie;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(MovieDomainSharedModule)
)]
public class MovieDomainModule : AbpModule
{

}
