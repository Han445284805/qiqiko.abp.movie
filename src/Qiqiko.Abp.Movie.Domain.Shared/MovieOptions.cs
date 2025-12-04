using System;
using System.Collections.Generic;
using System.Text;

namespace Qiqiko.Abp.Movie
{
    public class MovieOptions
    {
        /// <summary>
        /// 临时文件存放路径
        /// </summary>
        public string? TempBasePath {  get; set; }
        /// <summary>
        /// M3u8文件存放路径
        /// </summary>
        public string? M3u8PBasePath { get; set; }
    }
}
