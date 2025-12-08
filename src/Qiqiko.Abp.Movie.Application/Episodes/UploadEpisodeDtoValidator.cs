using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Qiqiko.Abp.Movie.Episodes;
using Qiqiko.Abp.Movie.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Qiqiko.Abp.Movie.Movies;

public class UploadEpisodeDtoValidator : AbstractValidator<UploadEpisodeDto>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<Episode, Guid> _repository;
    private readonly IStringLocalizer<MovieResource> _localizer;

    public UploadEpisodeDtoValidator(IHttpContextAccessor httpContextAccessor, IRepository<Episode, Guid> repository, IStringLocalizer<MovieResource> localizer)
    {
        _httpContextAccessor = httpContextAccessor;
        _repository = repository;
        _localizer = localizer;
        RuleFor(x => x).CustomAsync(CustomRuleAsync);
    }

    private async Task CustomRuleAsync(UploadEpisodeDto dto, ValidationContext<UploadEpisodeDto> context, CancellationToken token)
    {
        if (dto?.File == null || dto.File.Length == 0)
        {
            context.AddFailure("File", _localizer[MovieErrorCodes.UploadFileError]);
            return;
        }
        var isGuid = Guid.TryParse(Util.GetRouteParameter(_httpContextAccessor, "id"), out Guid episodeId);
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
    }
}