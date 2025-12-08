using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Qiqiko.Abp.Movie;

/// <summary>
/// 影片
/// </summary>
public partial class Movie : FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 序号
    /// </summary>
    public virtual int Index { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    public virtual string? Name { get; set; }
    /// <summary>
    /// 演员
    /// </summary>
    public virtual string? Performers { get; set; }
    /// <summary>
    /// 主演
    /// </summary>
    public virtual string? Star { get; set; }
    /// <summary>
    /// 视频类型
    /// </summary>
    public virtual Guid? MovieTypeId { get; set; }
    /// <summary>
    /// 视频类型
    /// </summary>
    public virtual MovieType? MovieType { get; set; }
    /// <summary>
    /// 总集数
    /// </summary>
    public virtual int EpisodeCount { get; set; }
    /// <summary>
    /// 标签
    /// </summary>
    [DisableAuditing]
    public virtual string? Tags { get; set; }
    /// <summary>
    /// 语言
    /// </summary>
    [DisableAuditing]
    public virtual string? Language { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    [DisableAuditing]
    public virtual string? Description { get; set; }
    /// <summary>
    /// 视频分级
    /// </summary>
    [DisableAuditing]
    public string? Rating { get; set; }
    /// <summary>
    /// 导演
    /// </summary>
    [DisableAuditing]
    public string? Director { get; set; }
    /// <summary>
    /// 上映日期
    /// </summary>
    [DisableAuditing]
    public DateTime? ReleaseDate { get; set; }
    public Movie()
    {

    }

    /// <summary>
    /// 影片
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="index">序号</param>
    /// <param name="name">名称</param>
    /// <param name="performers">演员</param>
    /// <param name="star">主演</param>
    /// <param name="movieTypeId">影片类型</param>
    /// <param name="episodeCount">分集数</param>
    /// <param name="tags">标签</param>
    /// <param name="language">语言</param>
    /// <param name="description">描述</param>
    public Movie(Guid id, int index, string name, string? performers, string? star, Guid? movieTypeId, int episodeCount, string? tags, string? language, string? description) : base(id)
    {
        Index = index;
        Name = name;
        Performers = performers;
        Star = star;
        MovieTypeId = movieTypeId;
        EpisodeCount = episodeCount;
        Tags = tags;
        Language = language;
        Description = description;
    }

    public void SetMovieTypeId(Guid? movieTypeId)
    {
        MovieTypeId = movieTypeId;
    }
}
