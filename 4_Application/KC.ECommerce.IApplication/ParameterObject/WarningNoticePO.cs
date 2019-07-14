using KC.ECommerce.Domain.Keys;

namespace KC.ECommerce.IApplication
{
    public class WarningNoticePO : PagedPO
    {
        public int Id { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public HandleStatus HandleStatus { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public string SmsContent { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string EmailContent { get; set; }

    }
}
