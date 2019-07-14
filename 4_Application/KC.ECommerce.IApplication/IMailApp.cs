namespace KC.ECommerce.IApplication
{
    public interface IMailApp : IBaseApp
    {
        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="to">收件人邮箱账号</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <returns></returns>
        bool SendEmail(string to, string subject, string body);

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="to">收件人邮箱账号</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <returns></returns>
        bool SendEmailAsyn(string to, string subject, string body);
    }
}
