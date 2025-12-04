using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Qiqiko.Abp.Movie
{
    public class MovieType :FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public virtual int Index { get;  set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public virtual string? Name{ get;  set; }
        public MovieType()
        {
            
        }

        public MovieType(Guid id,int index, string name):base(id)
        {
            Index = index;
            Name = name;
        }
    }
}
