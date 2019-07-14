using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace KC.ECommerce.Common
{
    public class MailHelper
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to">收件人邮箱账号</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isAsync">是否异步发送</param>
        /// <returns></returns>
        public bool SendEmail(string to, string subject, string body, bool isAsync)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                //邮箱的smtp地址
                smtpClient.Host = Server;
                //端口号
                smtpClient.Port = 25;
                //构建发件人的身份凭据类
                smtpClient.Credentials = new NetworkCredential(UserName, Password);
                //是否启用SSL
                smtpClient.EnableSsl = false;
                //构建消息类
                MailMessage message = new MailMessage();
                //设置优先级
                message.Priority = MailPriority.High;
                //消息发送人
                message.From = new MailAddress(UserName, Name, System.Text.Encoding.UTF8);
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
