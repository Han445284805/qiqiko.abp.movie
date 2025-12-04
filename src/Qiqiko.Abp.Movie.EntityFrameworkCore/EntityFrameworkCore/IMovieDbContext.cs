using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Qiqiko.Abp.Movie.EntityFrameworkCore;

[ConnectionStringName(MovieDbProperties.ConnectionStringName)]
public interface IMovieDbContext : IEfCoreDbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Episode‌> MovieEpisode‌s { get; set; }
    public DbSet<MovieType> MovieTypes { get; set; }
}
