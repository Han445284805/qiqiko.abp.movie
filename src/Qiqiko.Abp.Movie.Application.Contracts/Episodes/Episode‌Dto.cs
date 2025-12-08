using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Qiqiko.Abp.Movie.Episodes;

public class Episode‌Dto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    /// <summary>
    /// 影片Id
    /// </summary>
    public Guid MovieId { get; set; }
    /// <summary>
    /// 序号
    /// </summary>
    public int Index { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// m3u8路径
    /// </summary>
    public string? M3u8Path { get; set; }
    /// <summary>
    /// 切片状态
    /// </summary>
    public FFmpegStatus FFmpegStatus { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; private set; }
    /// <summary>
    /// 时间戳
    /// </summary>
    public required string ConcurrencyStamp { get; set; }
}
