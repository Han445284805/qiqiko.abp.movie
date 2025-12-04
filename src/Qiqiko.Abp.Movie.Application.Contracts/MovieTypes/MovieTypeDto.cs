using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Qiqiko.Abp.Movie.MovieTypes;

public class MovieTypeDto:FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    /// <summary>
    /// 排序
    /// </summary>
    public int Index { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// 时间戳
    /// </summary>
    public required string ConcurrencyStamp { get; set; }
}
