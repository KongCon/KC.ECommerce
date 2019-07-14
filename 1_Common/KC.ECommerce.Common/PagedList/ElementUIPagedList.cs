using System.Collections.Generic;

namespace KC.ECommerce.Common
{
    /// <summary>
    /// PageList封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ElementUIPagedList<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ElementUIPagedList()
        {
            this.Items = new List<T>();
        }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 当前页数据
        /// </summary>
        public IList<T> Items { get; set; }
    }
}
