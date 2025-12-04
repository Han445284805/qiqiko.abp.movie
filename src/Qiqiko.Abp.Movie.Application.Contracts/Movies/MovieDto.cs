using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Qiqiko.Abp.Movie.Movies;

public class MovieDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    /// <summary>
    /// 序号
    /// </summary>
    public int Index { get; set; }
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
    /// 视频类型
    /// </summary>
    public string? MovieTypeName { get; set; }
    /// <summary>
    /// 总集数
    /// </summary>
    public int EpisodeCount { get; set; }
    /// <summary>
    /// 标签
    /// </summary>
    public string? Tags { get; set; }
    /// <summary>
    /// 语言
    /// </summary>
    public string? Language { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 时间戳
    /// </summary>
    public required string ConcurrencyStamp { get; set; } 
}
