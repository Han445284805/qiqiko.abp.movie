using Qiqiko.Abp.Movie.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Qiqiko.Abp.Movie.MovieTypes
{
    public class MovieTypeAppService : CrudAppService<MovieType, MovieTypeDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMovieTypeDto>, IMovieTypeAppService
    {
        private readonly IRepository<Movie, Guid> _movieRepository;
        public MovieTypeAppService(IRepository<MovieType, Guid> repository, IRepository<Movie, Guid> movieRepository) : base(repository)
        {
            LocalizationResource = typeof(MovieResource);
            ObjectMapperContext = typeof(MovieApplicationModule);
            _movieRepository = movieRepository;
        }

        public override async Task DeleteAsync(Guid id)
        {
            if (await _movieRepository.AnyAsync(r => r.MovieTypeId == id))
                throw new BusinessException(MovieErrorCodes.MovieTypeIsUsing);
            await base.DeleteAsync(id);
        }
    }
}