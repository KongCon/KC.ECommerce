namespace KC.ECommerce.Domain.Entities
{
    /// <summary>
    /// 用户角色关联
    /// </summary>
    public class UserRole : BaseEntity
    {
        #region 基础属性
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 关联用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 关联角色
        /// </summary>
        public virtual Role Role { get; set; }

        #endregion

    }
}
