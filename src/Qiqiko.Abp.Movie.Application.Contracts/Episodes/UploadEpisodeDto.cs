using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Domain.Entities;

namespace Qiqiko.Abp.Movie.Episodes;

public class UploadEpisodeDto : IHasConcurrencyStamp
{
    /// <summary>
    /// 文件
    /// </summary>
    [FromForm] 
    public required IFormFile File { get; set; }
    /// <summary>
    /// 时间戳
    /// </summary>
    public required string ConcurrencyStamp { get; set; }
}
