namespace KC.ECommerce.Application
{
    public class WarningNoticeDTO
    {
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
        public string SendStatus { get; set; }

        /// <summary>
        /// 发送数量
        /// </summary>
        public int SendCount { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 是否发送邮件
        /// </summary>
        public string IsSendEmail { get; set; }

        /// <summary>
        /// 是否发送短信
        /// </summary>
        public string IsSendSms { get; set; }

        /// <summary>
        /// 是否处理
        /// </summary>
        public string HandleStatus { get; set; }
    }
}
