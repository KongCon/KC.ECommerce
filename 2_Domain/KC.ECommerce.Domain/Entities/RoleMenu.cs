namespace KC.ECommerce.Domain.Entities
{
    /// <summary>
    /// 角色菜单关联
    /// </summary>
    public class RoleMenu : BaseEntity
    {
        #region 基础属性
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuId { get; set; }
        #endregion

        #region 扩展属性

        /// <summary>
        /// 关联角色
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 关联菜单
        /// </summary>
        public virtual Menu Menu { get; set; }
        #endregion

    }
}
