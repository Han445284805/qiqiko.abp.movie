using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Qiqiko.Abp.Movie.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Qiqiko.Abp.Movie.Movies;

public class CreateUpdateMovieDtoValidator : AbstractValidator<CreateUpdateMovieDto>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<Movie, Guid> _repository;
    private readonly IRepository<MovieType, Guid> _movieTyieRepository;
    private readonly IStringLocalizer<MovieResource> _localizer;

    public CreateUpdateMovieDtoValidator(IHttpContextAccessor httpContextAccessor, IRepository<Movie, Guid> repository, IRepository<MovieType, Guid> movieTyieRepository, IStringLocalizer<MovieResource> localizer)
    {
        _httpContextAccessor = httpContextAccessor;
        _repository = repository;
        _movieTyieRepository = movieTyieRepository;
        _localizer = localizer;
        RuleFor(x => x).CustomAsync(CustomRuleAsync);
    }

    private async Task CustomRuleAsync(CreateUpdateMovieDto dto, ValidationContext<CreateUpdateMovieDto> context, CancellationToken token)
    {
        var id = Util.GetRouteParameter(_httpContextAccessor,"id");
        if (id.IsNullOrWhiteSpace())
        {
            var existingMovie = await _repository.FirstOrDefaultAsync(r => r.Name == dto.Name, cancellationToken: token);
            if (existingMovie != null)
            {
                context.AddFailure("Name", string.Format( _localizer[MovieErrorCodes.MovieNameAlreadyExists], dto.Name));
            }
        }
        else
        {
            var isGuid = Guid.TryParse(id,out Guid movieId);
            if (!isGuid)
            {
                context.AddFailure("id", _localizer[MovieErrorCodes.RouteIdIsNotGuid]);
                return;
            }
            if (!await _repository.AnyAsync(r => r.Id == movieId, cancellationToken: token))
            {
                context.AddFailure("id", _localizer[MovieErrorCodes.MovieNotExists]);
                return;
            }
            var existingMovie = await _repository.FirstOrDefaultAsync(r => r.Name == dto.Name && r.Id != movieId, cancellationToken: token);
            if (existingMovie != null)
            {
                context.AddFailure("Name", string.Format(_localizer[MovieErrorCodes.MovieNameAlreadyExists], dto.Name));
            }
        }
        if (dto.MovieTypeId.HasValue)
        {
            var existingMovieType = await _movieTyieRepository.FirstOrDefaultAsync(r => r.Id == dto.MovieTypeId, cancellationToken: token);
            if (existingMovieType == null)
            {
                context.AddFailure("MovieType", _localizer[MovieErrorCodes.MovieTypeNotExists]);
            }
        }
    }
}