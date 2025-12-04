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

public class SetMovieTypeDtoValidator : AbstractValidator<SetMovieTypeDto>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<MovieType, Guid> _movieTyieRepository;
    private readonly IRepository<Movie, Guid> _repository;
    private readonly IStringLocalizer<MovieResource> _localizer;

    public SetMovieTypeDtoValidator(IHttpContextAccessor httpContextAccessor, IRepository<MovieType, Guid> movieTyieRepository, IStringLocalizer<MovieResource> localizer, IRepository<Movie, Guid> repository)
    {
        _httpContextAccessor = httpContextAccessor;
        _movieTyieRepository = movieTyieRepository;
        _repository = repository;
        _localizer = localizer;
        RuleFor(x => x).CustomAsync(CustomRuleAsync);
    }

    private async Task CustomRuleAsync(SetMovieTypeDto dto, ValidationContext<SetMovieTypeDto> context, CancellationToken token)
    {
        var isGuid = Guid.TryParse(Util.GetRouteParameter(_httpContextAccessor, "id"), out Guid movieId);
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