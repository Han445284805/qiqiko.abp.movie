using Qiqiko.Abp.Movie.Localization;
using Volo.Abp.Application.Services;

namespace Qiqiko.Abp.Movie;

public abstract class MovieAppService : ApplicationService
{
    protected MovieAppService()
    {
        LocalizationResource = typeof(MovieResource);
        ObjectMapperContext = typeof(MovieApplicationModule);
    }
}
