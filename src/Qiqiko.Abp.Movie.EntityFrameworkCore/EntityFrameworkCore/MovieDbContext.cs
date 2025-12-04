using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Qiqiko.Abp.Movie.EntityFrameworkCore;

[ConnectionStringName(MovieDbProperties.ConnectionStringName)]
public class MovieDbContext : AbpDbContext<MovieDbContext>, IMovieDbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Episode‌> MovieEpisode‌s { get; set; }
    public DbSet<MovieType> MovieTypes { get; set; }

    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureMovie();
    }
}
