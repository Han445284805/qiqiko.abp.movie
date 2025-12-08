using Qiqiko.Abp.Movie.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Qiqiko.Abp.Movie.Movies
{
    public class MovieAppService : CrudAppService<Movie, MovieDto, Guid, MoviePageRequestDto, CreateUpdateMovieDto>, IMovieAppService
    {
        private static readonly char[] separator = [',', ';', '，'];
        public MovieAppService(IRepository<Movie, Guid> repository) : base(repository)
        {
            LocalizationResource = typeof(MovieResource);
            ObjectMapperContext = typeof(MovieApplicationModule);
        }

        protected override async Task<IQueryable<Movie>> CreateFilteredQueryAsync(MoviePageRequestDto input)
        {
            var queryable = await base.CreateFilteredQueryAsync(input);
            return queryable.WhereIf(!string.IsNullOrWhiteSpace(input.Name), m => m.Name!.Contains(input.Name!))
                .WhereIf(input.Performers?.Count > 0, m => input.Performers!.Any(r => m.Performers!.Contains(r)))
                .WhereIf(input.Star?.Where(r=>!string.IsNullOrWhiteSpace(r))?.Count() > 0, m => input.Star!.Where(r => !string.IsNullOrWhiteSpace(r)).Any(r => m.Star!.Contains(r)))
                .WhereIf(input.MovieTypeId.HasValue && input.MovieTypeId != Guid.Empty, m => m.MovieTypeId == input.MovieTypeId)
                .WhereIf(input.Tags?.Count > 0, m => input.Tags!.Any(r => m.Tags!.Contains(r)))
                .WhereIf(input.Language?.Count > 0, m => input.Language!.Any(r => m.Language!.Contains(r)))
                .WhereIf(input.Rating?.Count > 0, m => input.Rating!.Any(r => m.Rating!.Contains(r)))
                .WhereIf(input.Director?.Count > 0, m => input.Director!.Any(r => m.Director!.Contains(r)))
                .WhereIf(input.ReleaseDate?.Count > 0, m => m.ReleaseDate >= input.ReleaseDate![0].Date)
                .WhereIf(input.ReleaseDate?.Count > 1, m => m.ReleaseDate < input.ReleaseDate![1].Date);
        }
        public async Task<MovieDto> SetMovieTypeAsync(Guid id, SetMovieTypeDto input)
        {
            var entity = await GetEntityByIdAsync(id);
            entity.SetMovieTypeId(input.MovieTypeId);
            await Repository.UpdateAsync(entity, autoSave: true);
            return await MapToGetOutputDtoAsync(entity);
        }

        public async Task<List<string>> GetAllTagsAsync()
        {
            var query = await Repository.GetQueryableAsync();
            return [.. query
                .Where(m => !string.IsNullOrWhiteSpace(m.Tags))
                .AsEnumerable()
                .SelectMany(m => m.Tags!.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                .Distinct()];
        }

        public async Task<List<string>> GetAllPerformersAsync()
        {
            var query = await Repository.GetQueryableAsync();
            return [.. query
                .Where(m => !string.IsNullOrWhiteSpace(m.Performers+m.Star))
                .AsEnumerable()
                .SelectMany(m => (m.Performers + "," + m.Star).Split(separator, StringSplitOptions.RemoveEmptyEntries))
                .Distinct()];
        }
    }
}
