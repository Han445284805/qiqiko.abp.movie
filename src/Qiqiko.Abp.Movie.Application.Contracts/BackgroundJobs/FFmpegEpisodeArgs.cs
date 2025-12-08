using System;

namespace Qiqiko.Abp.Movie.BackgroundJobs
{
    /// <summary>
    /// 待切片分集
    /// </summary>
    public class FFmpegEpisodeArgs
    {
        /// <summary>
        /// 分集Id
        /// </summary>
        public Guid EpisodeId { get; set; }

    }
}
