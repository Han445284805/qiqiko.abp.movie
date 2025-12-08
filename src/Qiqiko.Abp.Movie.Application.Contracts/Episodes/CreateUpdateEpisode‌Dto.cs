using System;

namespace Qiqiko.Abp.Movie.Episodes;

public class CreateUpdateEpisode‌Dto
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
    /// 描述
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 时间戳
    /// </summary>
    public string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString("N");
}
