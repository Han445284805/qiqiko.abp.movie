using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Qiqiko.Abp.Movie.Localization;
using Qiqiko.Abp.Movie.MovieTypes;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Qiqiko.Abp.Movie.Movies;

public class CreateUpdateMovieTypeDtoValidator : AbstractValidator<CreateUpdateMovieTypeDto>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<MovieType, Guid> _repository;
    private readonly IStringLocalizer<MovieResource> _localizer;

    public CreateUpdateMovieTypeDtoValidator(IHttpContextAccessor httpContextAccessor, IRepository<MovieType, Guid> repository, IStringLocalizer<MovieResource> localizer)
    {
        _httpContextAccessor = httpContextAccessor;
        _repository = repository;
        _localizer = localizer;
        RuleFor(x => x).CustomAsync(CustomRuleAsync);
    }

    private async Task CustomRuleAsync(CreateUpdateMovieTypeDto dto, ValidationContext<CreateUpdateMovieTypeDto> context, CancellationToken token)
    {
        var id = Util.GetRouteParameter(_httpContextAccessor, "id");
        if (id.IsNullOrWhiteSpace())
        {
            var existingMovie = await _repository.FirstOrDefaultAsync(r => r.Name == dto.Name, cancellationToken: token);
            if (existingMovie != null)
            {
                context.AddFailure("Name", string.Format( _localizer[MovieErrorCodes.MovieTypeNameAlreadyExists], dto.Name));
            }
        }
        else
        {
            var isGuid = Guid.TryParse(id, out Guid movieId);
            if (!isGuid)
            {
                context.AddFailure("id", _localizer[MovieErrorCodes.RouteIdIsNotGuid]);
                return;
            }
            if (!await _repository.AnyAsync(r => r.Id == movieId, cancellationToken: token))
            {
                context.AddFailure("id", _localizer[MovieErrorCodes.MovieTypeNotExists]);
                return;
            }
            var existingMovie = await _repository.FirstOrDefaultAsync(r => r.Name == dto.Name && r.Id != movieId, cancellationToken: token);
            if (existingMovie != null)
            {
                context.AddFailure("Name", string.Format(_localizer[MovieErrorCodes.MovieTypeNameAlreadyExists], dto.Name));
            }
        }
    }
}