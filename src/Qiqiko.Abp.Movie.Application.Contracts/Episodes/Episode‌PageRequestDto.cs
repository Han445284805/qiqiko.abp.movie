using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Qiqiko.Abp.Movie.Episodes;

public class Episode‌PageRequestDto: PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 视频Id
    /// </summary>
    [Required(ErrorMessage ="视频Id不能为空")]
    public Guid MovieId {  get; set; }
}
