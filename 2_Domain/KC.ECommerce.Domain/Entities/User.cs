using System.Collections.Generic;

namespace KC.ECommerce.Domain.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class User: BaseEntity
    {
        #region 基础属性   
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码(此处为了方便采用明文，后期可以加密处理)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string Avatar { get; set; }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 用户关联角色
        /// </summary>
        public virtual IList<UserRole> UserRoleList { get; set; }
        #endregion
    }
}
