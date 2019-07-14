namespace KC.ECommerce.Application
{
    public class SMSSetting
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        public string BaseAddress { get; set; }

        /// <summary>
        /// 任务命令
        /// </summary>
        public string SendAction { get; set; }
    }
}
