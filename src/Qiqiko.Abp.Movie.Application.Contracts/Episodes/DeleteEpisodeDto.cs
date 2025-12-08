using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Qiqiko.Abp.Movie.Episodes;

public class DeleteEpisodeDto : IHasConcurrencyStamp
{
    /// <summary>
    /// 时间戳
    /// </summary>
    [Required(ErrorMessage = "时间戳不可为空")]
    public required string ConcurrencyStamp { get; set; }
}
