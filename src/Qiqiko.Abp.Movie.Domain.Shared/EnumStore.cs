using System.ComponentModel.DataAnnotations;

namespace Qiqiko.Abp.Movie;

/// <summary>
/// 切片状态
/// </summary>
public enum FFmpegStatus
{
    /// <summary>
    /// 未上传
    /// </summary>
    [Display(Name = "未上传")]
    UnUpLoad = -1,
    /// <summary>
    /// 切片中
    /// </summary>
    [Display(Name = "切片中")]
    Processing = 0,
    /// <summary>
    /// 已完成
    /// </summary>
    [Display(Name = "已完成")]
    Finished = 1,
    /// <summary>
    /// 失败
    /// </summary>
    [Display(Name = "失败")]
    Failed = 2,
}
