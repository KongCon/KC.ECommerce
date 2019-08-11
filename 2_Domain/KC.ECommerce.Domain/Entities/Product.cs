using KC.ECommerce.Domain.Keys;

namespace KC.ECommerce.Domain.Entities
{
    public partial class Product : BaseEntity
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品简述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 产品状态
        /// </summary>
        public ProductStatus Status { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sequence { get; set; }
    }
}
