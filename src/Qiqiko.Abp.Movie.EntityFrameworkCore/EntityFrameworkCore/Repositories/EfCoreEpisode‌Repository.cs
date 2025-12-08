using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Qiqiko.Abp.Movie.EntityFrameworkCore.Repositories;

public class EfCoreEpisode‌Repository : EfCoreRepository<MovieDbContext, Episode‌, Guid>, IEpisodeRepository
{
    public EfCoreEpisode‌Repository(IDbContextProvider<MovieDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

}
