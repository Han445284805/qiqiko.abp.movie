using Microsoft.AspNetCore.Mvc;
using Qiqiko.Abp.Movie.Movies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Qiqiko.Abp.Movie;

/// <summary>
///  视频
/// </summary>
[Area(MovieRemoteServiceConsts.ModuleName)]
[RemoteService(Name = MovieRemoteServiceConsts.RemoteServiceName)]
[Route("api/movie")]
public class MovieController : QiqikoMovieController
{
    private readonly IMovieAppService _qiqikoMovieAppService;
    public MovieController(IMovieAppService qiqikoMovieAppService)
    {
        _qiqikoMovieAppService = qiqikoMovieAppService;
    }

    /// <summary>
    /// 新建视频
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    public Task<MovieDto> CreateAsync([FromBody] CreateUpdateMovieDto input) => _qiqikoMovieAppService.CreateAsync(input);

    /// <summary>
    /// 删除视频
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id) => _qiqikoMovieAppService.DeleteAsync(id);

    /// <summary>
    /// 获取视频
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("{id}")]
    public Task<MovieDto> GetAsync(Guid id) => _qiqikoMovieAppService.GetAsync(id);
    /// <summary>
    /// 获取视频分页列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("items")]
    public Task<PagedResultDto<MovieDto>> GetListAsync([FromQuery] MoviePageRequestDto input) => _qiqikoMovieAppService.GetListAsync(input);

    /// <summary>
    /// 设置视频分类
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut("set-type/{id}")]
    public Task<MovieDto> SetMovieTypeAsync(Guid id, SetMovieTypeDto input) => _qiqikoMovieAppService.SetMovieTypeAsync(id, input);

    /// <summary>
    /// 修改视频
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut("{id}")]
    public Task<MovieDto> UpdateAsync(Guid id, CreateUpdateMovieDto input) => _qiqikoMovieAppService.UpdateAsync(id, input);

    /// <summary>
    /// 查询所有标签
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("all-tags")]
    public Task<List<string>> GetAllTagsAsync() => _qiqikoMovieAppService.GetAllTagsAsync();

    /// <summary>
    /// 查询所有演员(包含主演)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("all-performers")]
    public Task<List<string>> GetAllPerformersAsync() => _qiqikoMovieAppService.GetAllTagsAsync();
}