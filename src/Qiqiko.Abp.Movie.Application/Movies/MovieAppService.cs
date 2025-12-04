using Qiqiko.Abp.Movie.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Qiqiko.Abp.Movie.Movies
{
    public class MovieAppService : CrudAppService<Movie, MovieDto, Guid, MoviePageRequestDto, CreateUpdateMovieDto>, IMovieAppService
    {
        public MovieAppService(IRepository<Movie, Guid> repository) : base(repository)
        {
            LocalizationResource = typeof(MovieResource);
            ObjectMapperContext = typeof(MovieApplicationModule);
        }

        protected override async Task<IQueryable<Movie>> CreateFilteredQueryAsync(MoviePageRequestDto input)
        {
            var queryable = await base.CreateFilteredQueryAsync(input);
            return queryable.WhereIf(!string.IsNullOrWhiteSpace(input.Name), m => m.Name!.Contains(input.Name!))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Performers), m => m.Performers!.Contains(input.Performers!))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Star), m => m.Star!.Contains(input.Star!))
                .WhereIf(input.MovieTypeId.HasValue && input.MovieTypeId != Guid.Empty, m => m.MovieTypeId == input.MovieTypeId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Tags), m => m.Tags!.Contains(input.Tags!))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Language), m => m.Language!.Contains(input.Language!));
        }
        public async Task<MovieDto> SetMovieTypeAsync(Guid id, SetMovieTypeDto input)
        {
            var entity = await GetEntityByIdAsync(id);
            entity.SetMovieTypeId(input.MovieTypeId);
            await Repository.UpdateAsync(entity, autoSave: true);
            return await MapToGetOutputDtoAsync(entity);
        }

        private static readonly char[] separator = [',', ';','，'];

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
