using KC.ECommerce.Common;
using KC.ECommerce.IApplication;
using KC.ECommerce.IRepository;

namespace KC.ECommerce.Application
{
    public class UserApp : BaseApp, IUserApp
    {
        #region 字段
        private readonly IUserRepository _userRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region 构造函数
        public UserApp(IUserRepository userRepository, IWorkContext workContext)
        {
            this._workContext = workContext;
            _userRepository = userRepository;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取当前登陆用户信息
        /// </summary>
        /// <param name="qc"></param>
        /// <returns></returns>
        public ResponseResultBase GetUserInfo()
        {
            var response = new ResponseResultBase();
            var user = _workContext.CurrentUser;
            if (user != null)
            {
                response.Data = user;
            }
            else
            {
                response.SetFailed("用户未登录或登陆超时，请重新登陆", ErrorCode.NoPermission);
            }
            return response;
        }
        #endregion
    }
}
