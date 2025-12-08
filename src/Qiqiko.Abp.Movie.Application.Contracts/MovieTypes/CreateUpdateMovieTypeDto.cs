using System.ComponentModel.DataAnnotations;

namespace Qiqiko.Abp.Movie.MovieTypes;

public class CreateUpdateMovieTypeDto
{
    /// <summary>
    /// 排序
    /// </summary>
    public int Index { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "分类不能为空")]
    public required string Name { get; set; }
}
