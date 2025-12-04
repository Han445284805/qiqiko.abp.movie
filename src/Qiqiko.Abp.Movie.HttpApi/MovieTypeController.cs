using Microsoft.AspNetCore.Mvc;
using Qiqiko.Abp.Movie.Localization;
using Qiqiko.Abp.Movie.Movies;
using Qiqiko.Abp.Movie.MovieTypes;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Qiqiko.Abp.Movie;

/// <summary>
///  视频分类
/// </summary>
[Area(MovieRemoteServiceConsts.ModuleName)]
[RemoteService(Name = MovieRemoteServiceConsts.RemoteServiceName)]
[Route("api/movie-type")]
public class MovieTypeController : QiqikoMovieController
{
    private readonly IMovieTypeAppService _qiqikoMovieTypeAppService;
    public MovieTypeController(IMovieTypeAppService qiqikoMovieTypeAppService)
    {
        _qiqikoMovieTypeAppService = qiqikoMovieTypeAppService;
    }

    /// <summary>
    /// 新建视频分类
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    public Task<MovieTypeDto> CreateAsync([FromBody] CreateUpdateMovieTypeDto input)=> _qiqikoMovieTypeAppService.CreateAsync(input);

    /// <summary>
    /// 删除视频分类
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id) => _qiqikoMovieTypeAppService.DeleteAsync(id);

    /// <summary>
    /// 获取视频分类
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("{id}")]
    public Task<MovieTypeDto> GetAsync(Guid id) => _qiqikoMovieTypeAppService.GetAsync(id);
    /// <summary>
    /// 获取视频分类分页列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("items")]
    public Task<PagedResultDto<MovieTypeDto>> GetListAsync([FromQuery]MoviePageRequestDto input) => _qiqikoMovieTypeAppService.GetListAsync(input);
    /// <summary>
    /// 修改视频分类
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut("{id}")]
    public Task<MovieTypeDto> UpdateAsync(Guid id,CreateUpdateMovieTypeDto input) => _qiqikoMovieTypeAppService.UpdateAsync(id, input);
}