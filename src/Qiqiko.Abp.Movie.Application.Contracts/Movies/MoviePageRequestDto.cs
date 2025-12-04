using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace Qiqiko.Abp.Movie.Movies;

public class MoviePageRequestDto: PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 演员
    /// </summary>
    public string? Performers { get; set; }
    /// <summary>
    /// 主演
    /// </summary>
    public string? Star { get; set; }
    /// <summary>
    /// 视频类型
    /// </summary>
    public Guid? MovieTypeId { get; set; }
    /// <summary>
    /// 标签
    /// </summary>
    public  string? Tags { get; set; }
    /// <summary>
    /// 语言
    /// </summary>
    public string? Language { get; set; }

}
