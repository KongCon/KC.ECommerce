using System.Collections.Generic;

namespace KC.ECommerce.Domain.Entities
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : BaseEntity
    {
        #region 基础属性
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string ICON { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Discription { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        #endregion

        #region 扩展属性
        /// <summary>
        /// 菜单关联角色
        /// </summary>
        public virtual IList<RoleMenu> RoleMenuList { get; set; }
        #endregion
    }
}
