using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Qiqiko.Abp.Movie.Movies;

public class CreateUpdateMovieDto 
{
    /// <summary>
    /// 序号
    /// </summary>
    public int Index { get;  set; }
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage ="视频名称不能为空")]
    public string? Name { get;  set; }
    /// <summary>
    /// 演员
    /// </summary>
    public string? Performers { get;  set; }
    /// <summary>
    /// 主演
    /// </summary>
    public string? Star { get;  set; }
    /// <summary>
    /// 视频类型
    /// </summary>
    public Guid? MovieTypeId { get;  set; }
    /// <summary>
    /// 标签
    /// </summary>
    public string? Tags { get;  set; }
    /// <summary>
    /// 语言
    /// </summary>
    public string? Language { get;  set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get;  set; }
    /// <summary>
    /// 时间戳
    /// </summary>
    public string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString("N");
}
