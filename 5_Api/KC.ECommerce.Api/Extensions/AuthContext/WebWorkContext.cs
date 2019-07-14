using KC.ECommerce.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace KC.ECommerce.Api.Extensions.AuthContext
{
    public class WebWorkContext : IWorkContext
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        private AuthorizedUser _user;

        public WebWorkContext(IHttpContextAccessor httpContextAccessor)
        {
            this._claimsPrincipal = httpContextAccessor.HttpContext.User;
        }
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public AuthorizedUser CurrentUser
        {
            get
            {
                if (_user == null)
                {
                    if (_claimsPrincipal?.Identity?.IsAuthenticated ?? false)
                    {
                        _user = new AuthorizedUser()
                        {
                            UserId = Convert.ToInt32(_claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)),
                            Account = _claimsPrincipal.FindFirstValue("account"),
                            Name = _claimsPrincipal.FindFirstValue(ClaimTypes.Name),
                            IsAdmin = Convert.ToBoolean(_claimsPrincipal.FindFirstValue("isAdmin")),
                            Avatar = _claimsPrincipal.FindFirstValue("avatar")
                        };
                    }
                }
                return _user;
            }
        }
    }
}
