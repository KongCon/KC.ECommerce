﻿namespace KC.ECommerce.IApplication
{
    public class WarningNoticeConfigVO
    {
        #region Base属性
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
        /// sql脚本
        /// </summary>
        public string SqlScript { get; set; }
        #endregion
    }
}
