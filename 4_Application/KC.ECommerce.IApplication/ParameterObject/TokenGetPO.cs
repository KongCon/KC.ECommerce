namespace KC.ECommerce.IApplication
{
    /// <summary>
    /// token获取参数
    /// </summary>
    public class TokenGetPO : BasePO
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
