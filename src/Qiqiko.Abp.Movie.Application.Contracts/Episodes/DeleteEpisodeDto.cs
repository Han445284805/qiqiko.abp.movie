using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
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
