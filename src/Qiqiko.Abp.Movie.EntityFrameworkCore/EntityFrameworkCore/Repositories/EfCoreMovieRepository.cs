using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Qiqiko.Abp.Movie.EntityFrameworkCore.Repositories;

public class EfCoreMovieRepository : EfCoreRepository<MovieDbContext, Movie, Guid>, IMovieRepository
{
    public EfCoreMovieRepository(IDbContextProvider<MovieDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

}
