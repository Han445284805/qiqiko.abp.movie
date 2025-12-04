using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Qiqiko.Abp.Movie.EntityFrameworkCore.Repositories;

public class EfCoreMovieTypeRepository : EfCoreRepository<MovieDbContext, MovieType, Guid>, IMovieTypeRepository
{
    public EfCoreMovieTypeRepository(IDbContextProvider<MovieDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

}
