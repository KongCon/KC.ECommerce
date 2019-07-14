using System.Collections.Generic;

namespace KC.ECommerce.IApplication
{
    /// <summary>
    /// 处理状态更改参数对象
    /// </summary>
    public class ChangeHandleStatusPO:BasePO
    {
        /// <summary>
        /// 预警通知ID
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public string SMSContent { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string EmailContent { get; set; }
    }
}
