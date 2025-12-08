using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Qiqiko.Abp.Movie;

[DependsOn(
    typeof(MovieDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class MovieApplicationContractsModule : AbpModule
{

}
