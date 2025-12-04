using Qiqiko.Abp.Movie.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Qiqiko.Abp.Movie;

public abstract class QiqikoMovieController : AbpControllerBase
{
    protected QiqikoMovieController()
    {
        LocalizationResource = typeof(MovieResource);
    }
}
