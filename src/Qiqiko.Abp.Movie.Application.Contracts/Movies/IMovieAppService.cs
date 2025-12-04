using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;


namespace Qiqiko.Abp.Movie.Movies;

/// <summary>
///  视频
/// </summary>
public interface IMovieAppService : ICrudAppService<
    MovieDto,
    Guid,
    MoviePageRequestDto,
    CreateUpdateMovieDto>, IRemoteService
{

    /// <summary>
    /// 设置视频分类
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Task<MovieDto> SetMovieTypeAsync(Guid id, SetMovieTypeDto input);
    /// <summary>
    /// 查询所有标签
    /// </summary>
    /// <returns></returns>
    public Task<List<string>> GetAllTagsAsync();
    /// <summary>
    /// 查询所有演员(包含主演)
    /// </summary>
    /// <returns></returns>
    public Task<List<string>> GetAllPerformersAsync();
}