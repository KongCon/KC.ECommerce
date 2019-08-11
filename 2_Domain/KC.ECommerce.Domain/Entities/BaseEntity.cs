using System;
using System.ComponentModel.DataAnnotations;

namespace KC.ECommerce.Domain.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
