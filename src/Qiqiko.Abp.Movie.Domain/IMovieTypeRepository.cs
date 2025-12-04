using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Qiqiko.Abp.Movie
{
    public interface IMovieTypeRepository : IBasicRepository<MovieType, Guid>
    {
    }
}
