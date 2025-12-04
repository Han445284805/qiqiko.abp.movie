using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FluentValidation;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;

namespace Qiqiko.Abp.Movie;

[DependsOn(
    typeof(MovieDomainModule),
    typeof(MovieApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule),
    typeof(AbpFluentValidationModule),
    typeof(AbpBackgroundJobsAbstractionsModule)
    )]
public class MovieApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<MovieApplicationModule>();
    }
}
