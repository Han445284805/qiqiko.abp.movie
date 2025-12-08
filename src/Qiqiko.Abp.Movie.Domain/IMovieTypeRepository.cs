using System;
using Volo.Abp.Domain.Repositories;

namespace Qiqiko.Abp.Movie
{
    public interface IMovieTypeRepository : IBasicRepository<MovieType, Guid>
    {
    }
}
