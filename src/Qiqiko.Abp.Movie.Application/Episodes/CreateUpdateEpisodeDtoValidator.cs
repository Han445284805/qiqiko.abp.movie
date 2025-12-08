using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Qiqiko.Abp.Movie.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Qiqiko.Abp.Movie.Episodes;

public class CreateUpdateEpisodeDtoValidator : AbstractValidator<CreateUpdateEpisodeDto>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<Episode, Guid> _repository;
    private readonly IRepository<Movie, Guid> _movieRepository;
    private readonly IStringLocalizer<MovieResource> _localizer;

    public CreateUpdateEpisodeDtoValidator(IHttpContextAccessor httpContextAccessor, IRepository<Episode, Guid> repository, IStringLocalizer<MovieResource> localizer, IRepository<Movie, Guid> movieRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _repository = repository;
        _localizer = localizer;
        _movieRepository = movieRepository;
        RuleFor(x => x).CustomAsync(CustomRuleAsync);
    }

    private async Task CustomRuleAsync(CreateUpdateEpisodeDto dto, ValidationContext<CreateUpdateEpisodeDto> context, CancellationToken token)
    {
        var id = Util.GetRouteParameter(_httpContextAccessor, "id");
        if (id.IsNullOrWhiteSpace())
        {
            var existingEpisode = await _repository.FirstOrDefaultAsync(r => r.Name == dto.Name, cancellationToken: token);
            if (existingEpisode != null)
            {
                context.AddFailure("Name", string.Format(_localizer[MovieErrorCodes.EpisodeNameAlreadyExists], dto.Name));
            }
        }
        else
        {
            var isGuid = Guid.TryParse(id, out Guid episodeId);
            if (!isGuid)
            {
                context.AddFailure("id", _localizer[MovieErrorCodes.RouteIdIsNotGuid]);
                return;
            }
            if (!await _repository.AnyAsync(r => r.Id == episodeId, cancellationToken: token))
            {
                context.AddFailure("id", _localizer[MovieErrorCodes.EpisodeNotExists]);
                return;
            }
            if (await _repository.AnyAsync(r => r.Name == dto.Name && r.Id != episodeId, cancellationToken: token))
            {
                context.AddFailure("Name", string.Format(_localizer[MovieErrorCodes.EpisodeNameAlreadyExists], dto.Name));
                return;
            }
        }
        if (!await _movieRepository.AnyAsync(r => r.Id == dto.MovieId, cancellationToken: token))
        {
            context.AddFailure("MovieId", _localizer[MovieErrorCodes.MovieNotExists]);
        }
    }
}