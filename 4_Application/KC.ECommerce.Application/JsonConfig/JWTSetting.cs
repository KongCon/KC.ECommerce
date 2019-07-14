using System;
using System.Collections.Generic;
using System.Text;

namespace KC.ECommerce.Application
{
    public class JWTSetting
    {
        /// <summary>
        /// 加密KEY
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Token过期时间：分钟
        /// </summary>
        public int ExpireMinutes { get; set; }
    }
}
