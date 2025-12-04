using System;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;


namespace Qiqiko.Abp.Movie.MovieTypes;
/// <summary>
///  视频类型
/// </summary>
public interface IMovieTypeAppService : ICrudAppService<
    MovieTypeDto,
    Guid,
    PagedAndSortedResultRequestDto,
    CreateUpdateMovieTypeDto>, IRemoteService
{
}