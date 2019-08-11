using System.Collections.Generic;

namespace KC.ECommerce.Domain.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    public partial class Role : BaseEntity
    {
        #region Base属性   
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

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
        /// 角色关联用户
        /// </summary>
        public virtual IList<UserRole> UserRoleList { get; set; }

        /// <summary>
        /// 角色关联菜单
        /// </summary>
        public virtual IList<RoleMenu> RoleMenuList { get; set; }

        #endregion
    }
}
