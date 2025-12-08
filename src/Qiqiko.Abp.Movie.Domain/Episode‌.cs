using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Qiqiko.Abp.Movie;

/// <summary>
/// 影片分集
/// </summary>
public class Episode‌ : FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 影片Id
    /// </summary>
    public virtual Guid MovieId { get; set; }
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
    /// 临时文件路径
    /// </summary>
    public virtual string? TempPath { get; set; }
    /// <summary>
    /// 原文件Hash
    /// </summary>
    public virtual string? OriginalHash { get; set; }
    /// <summary>
    /// m3u8路径
    /// </summary>
    public virtual string? M3u8Path { get; set; }
    /// <summary>
    /// 切片状态
    /// </summary>
    public virtual FFmpegStatus FFmpegStatus { get; set; } = FFmpegStatus.UnUpLoad;
    /// <summary>
    /// 描述
    /// </summary>
    [DisableAuditing]
    public virtual string? Description { get; set; }

    public Episode‌() { }
    public Episode‌(Guid id, Guid movieId, int index, string name, string? description) : base(id)
    {
        MovieId = movieId;
        Index = index;
        Name = name;
        Description = description;
    }

    /// <summary>
    /// 设置上传文件
    /// </summary>
    /// <param name="v"></param>
    /// <param name="fileName"></param>
    public void SetPath(string hash, string fileName, string concurrencyStamp)
    {
        TempPath = fileName;
        OriginalHash = hash;
        FFmpegStatus = FFmpegStatus.Processing;
        M3u8Path = string.Empty;
        ConcurrencyStamp = concurrencyStamp;
    }

    /// <summary>
    /// 设置切片完成
    /// </summary>
    /// <param name="m3u8Path"></param>
    public void CompletFFmpeg(string? m3u8Path)
    {
        FFmpegStatus = FFmpegStatus.Finished;
        M3u8Path = m3u8Path;
        TempPath = string.Empty;
    }

    /// <summary>
    /// 变为未上传状态
    /// </summary>
    public void ChangeUnLoad()
    {
        TempPath = string.Empty;
        OriginalHash = string.Empty;
        FFmpegStatus = FFmpegStatus.UnUpLoad;
        M3u8Path = string.Empty;
    }
}
