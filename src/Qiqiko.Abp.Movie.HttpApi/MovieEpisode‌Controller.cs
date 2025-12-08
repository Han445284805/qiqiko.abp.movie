using Microsoft.AspNetCore.Mvc;
using Qiqiko.Abp.Movie.Episode‌s;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Qiqiko.Abp.Movie;

/// <summary>
///  视频分集
/// </summary>
[Area(MovieRemoteServiceConsts.ModuleName)]
[RemoteService(Name = MovieRemoteServiceConsts.RemoteServiceName)]
[Route("api/movie-episode‌")]
public class MovieEpisode‌Controller : QiqikoMovieController
{
    private readonly IEpisode‌AppService _qiqikoEpisode‌AppService;
    public MovieEpisode‌Controller(IEpisode‌AppService qiqikoEpisode‌AppService)
    {
        _qiqikoEpisode‌AppService = qiqikoEpisode‌AppService;
    }

    /// <summary>
    /// 新建视频分集
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    public Task<Episode‌Dto> CreateAsync([FromBody] CreateUpdateEpisode‌Dto input) => _qiqikoEpisode‌AppService.CreateAsync(input);

    /// <summary>
    /// 删除视频分集
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id) => _qiqikoEpisode‌AppService.DeleteAsync(id);

    /// <summary>
    /// 获取视频分集
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("{id}")]
    public Task<Episode‌Dto> GetAsync(Guid id) => _qiqikoEpisode‌AppService.GetAsync(id);
    /// <summary>
    /// 获取视频分集分页列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("items")]
    public Task<PagedResultDto<Episode‌Dto>> GetListAsync([FromQuery] Episode‌PageRequestDto input) => _qiqikoEpisode‌AppService.GetListAsync(input);
    /// <summary>
    /// 修改视频分集
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut("{id}")]
    public Task<Episode‌Dto> UpdateAsync(Guid id, CreateUpdateEpisode‌Dto input) => _qiqikoEpisode‌AppService.UpdateAsync(id, input);
    /// <summary>
    /// 上传分集视频文件
    /// </summary>
    /// <param name="id">分集Id</param>
    /// <param name="input">视频文件</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut("/api/movie-episode/upload/{id}")]
    [RequestSizeLimit(int.MaxValue)]
    public Task<Episode‌Dto> UploadEpisodeAsync([FromRoute] Guid id, [FromForm] UploadEpisodeDto input) => _qiqikoEpisode‌AppService.UploadEpisodeAsync(id, input);
}