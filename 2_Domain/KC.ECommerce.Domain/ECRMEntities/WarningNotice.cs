using KC.ECommerce.Domain.Keys;
using System;

namespace KC.ECommerce.Domain
{
    public class WarningNotice
    {
        #region Base属性

        public int Id { get; set; }

        /// <summary>
        /// 配置类型
        /// </summary>
        public string ConfigType { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public string SmsContent { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string EmailContent { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public bool SendStatus { get; set; }

        /// <summary>
        /// 发送次数
        /// </summary>
        public int SendCount { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否发送邮件
        /// </summary>
        public bool IsSendEmail { get; set; }

        /// <summary>
        /// 是否发送短信
        /// </summary>
        public bool IsSendSms { get; set; }

        /// <summary>
        /// 是否处理
        /// </summary>
        public HandleStatus HandleStatus { get; set; }
        #endregion
    }
}
