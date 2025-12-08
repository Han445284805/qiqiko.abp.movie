using Microsoft.Extensions.DependencyInjection;
using Qiqiko.Abp.Movie.EntityFrameworkCore.Repositories;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Qiqiko.Abp.Movie.EntityFrameworkCore;

[DependsOn(
    typeof(MovieDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class MovieEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MovieDbContext>(options =>
        {
            options.AddDefaultRepositories<IMovieDbContext>(includeAllEntities: true);
            options.AddRepository<Movie, EfCoreMovieRepository>();
            options.AddRepository<Episode‌, EfCoreEpisode‌Repository>();
            options.AddRepository<MovieType, EfCoreMovieTypeRepository>();
        });
    }
}
