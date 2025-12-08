using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Qiqiko.Abp.Movie.Movies;

public class MoviePageRequestDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 演员
    /// </summary>
    public List<string>? Performers { get; set; }
    /// <summary>
    /// 主演
    /// </summary>
    public List<string>? Star { get; set; }
    /// <summary>
    /// 视频类型
    /// </summary>
    public Guid? MovieTypeId { get; set; }
    /// <summary>
    /// 标签
    /// </summary>
    public List<string>? Tags { get; set; }
    /// <summary>
    /// 语言
    /// </summary>
    public List<string>? Language { get; set; }
    /// <summary>
    /// 视频分级
    /// </summary>
    public List<string>? Rating { get; set; }
    /// <summary>
    /// 导演
    /// </summary>
    public List<string>? Director { get; set; }
    /// <summary>
    /// 上映日期
    /// </summary>
    public List<DateTime>? ReleaseDate { get; set; }

}
