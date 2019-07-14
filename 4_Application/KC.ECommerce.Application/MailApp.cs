using KC.ECommerce.IApplication;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace KC.ECommerce.Application
{
    public class MailApp : BaseApp, IMailApp
    {
        private readonly MailSetting _maillSetting;
        public MailApp(IOptions<MailSetting> mailSettingsOption)
        {
            this._maillSetting = mailSettingsOption.Value;
        }

        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="to">收件人邮箱账号</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <returns></returns>
        public bool SendEmail(string to, string subject, string body)
        {
            return this.SendEmail(to, subject, body, false);
        }

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="to">收件人邮箱账号</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <returns></returns>
        public bool SendEmailAsyn(string to, string subject, string body)
        {
            return this.SendEmail(to, subject, body, true);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isAsync"></param>
        /// <returns></returns>
        private bool SendEmail(string to, string subject, string body, bool isAsync)
        {
            SmtpClient smtpClient = new SmtpClient();
            //邮箱的smtp地址
            smtpClient.Host = _maillSetting.Server;
            //端口号
            smtpClient.Port = 25;
            //构建发件人的身份凭据类
            smtpClient.Credentials = new NetworkCredential(_maillSetting.UserName, _maillSetting.Password);
            //是否启用SSL
            smtpClient.EnableSsl = false;
            //构建消息类
            MailMessage message = new MailMessage();
            //设置优先级
            message.Priority = MailPriority.High;
            //消息发送人
            message.From = new MailAddress(_maillSetting.UserName, _maillSetting.Name, System.Text.Encoding.UTF8);
            //收件人
            message.To.Add(to);
            //标题
            message.Subject = subject.Trim();
            //标题字符编码
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            //正文
            message.Body = body.Trim();
            message.IsBodyHtml = true;
            //内容字符编码
            message.BodyEncoding = System.Text.Encoding.UTF8;

            //发送
            if (isAsync)
            {
                //异步发送邮件
                Task.Factory.StartNew(() =>
                {
                    smtpClient.Send(message);
                });
            }
            else
            {
                //同步发送邮件
                smtpClient.Send(message);
            }
            return true;
        }
    }
}
