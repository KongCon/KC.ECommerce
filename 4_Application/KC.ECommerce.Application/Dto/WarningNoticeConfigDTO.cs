namespace KC.ECommerce.Application
{
    public class WarningNoticeConfigDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string TypeDescription { get; set; }

        /// <summary>
        /// 默认短信内容
        /// </summary>
        public string DefaultSmsContent { get; set; }

        /// <summary>
        /// 默认邮件内容
        /// </summary>
        public string DefaultEmailContent { get; set; }

        /// <summary>
        /// 短信账号
        /// </summary>
        public string SmsAccounts { get; set; }

        /// <summary>
        /// 邮箱账号
        /// </summary>
        public string EmailAccounts { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// sql脚本
        /// </summary>
        public string SqlScript { get; set; }
    }
}
