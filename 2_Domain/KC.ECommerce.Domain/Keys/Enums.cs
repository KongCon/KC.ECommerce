using System.ComponentModel;

namespace KC.ECommerce.Domain.Keys
{
    /// <summary>
    /// 产品状态
    /// </summary>
    public enum ProductStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        [Description("所有")]
        All = 0,

        /// <summary>
        /// 就绪
        /// </summary>
        [Description("就绪")]
        Ready = 1,

        /// <summary>
        /// 已上架
        /// </summary>
        [Description("已上架")]
        OnShelf = 2,

        /// <summary>
        /// 已下架
        /// </summary>
        [Description("已下架")]
        Dismount = 3
    }
}
