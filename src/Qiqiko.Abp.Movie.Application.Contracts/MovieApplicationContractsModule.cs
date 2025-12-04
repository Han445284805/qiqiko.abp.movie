using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Qiqiko.Abp.Movie;

[DependsOn(
    typeof(MovieDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class MovieApplicationContractsModule : AbpModule
{

}
