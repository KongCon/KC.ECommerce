using KC.ECommerce.Domain.Keys;

namespace KC.ECommerce.IApplication.ParameterObject
{
    /// <summary>
    /// 产品列表查询参数对象
    /// </summary>
    public class ProductListPO:PagedPO
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品状态
        /// </summary>
        public ProductStatus Status { get; set; }
    }
}
