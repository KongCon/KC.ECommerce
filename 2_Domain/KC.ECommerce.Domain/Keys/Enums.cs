using System.ComponentModel;

namespace KC.ECommerce.Domain.Keys
{
    public enum HandleStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        [Description("所有")]
        All = 0,

        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        UnHandled =1,

        /// <summary>
        /// 已处理
        /// </summary>
        [Description("已处理")]
        Handled = 2
    }
}
