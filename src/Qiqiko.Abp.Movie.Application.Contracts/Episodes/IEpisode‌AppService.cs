using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;


namespace Qiqiko.Abp.Movie.Episodes;
/// <summary>
///  视频分集
/// </summary>
public interface IEpisode‌AppService : ICrudAppService<
    Episode‌Dto,
    Guid,
    Episode‌PageRequestDto,
    CreateUpdateEpisode‌Dto>, IRemoteService
{
    Task<EpisodeDto> UploadEpisodeAsync(Guid id, UploadEpisodeDto input);
}