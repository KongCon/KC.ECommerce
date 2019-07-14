using System;

namespace KC.ECommerce.Domain
{
    public class EntityBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreatedUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        public int UpdatedUserId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
