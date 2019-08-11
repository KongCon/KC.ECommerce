using KC.ECommerce.Common;
using KC.ECommerce.IApplication;
using KC.ECommerce.IRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace KC.ECommerce.Application
{
    public class TokenApp : BaseApp, ITokenApp
    {
        #region 字段
        private readonly IUserRepository _userRepository;
        private readonly JWTSetting _jwtSetting;
        #endregion

        #region 构造函数
        public TokenApp(IOptions<JWTSetting> jwtSettingsOption, IUserRepository userRepository)
        {
            this._jwtSetting = jwtSettingsOption.Value;
            this._userRepository = userRepository;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 用户登陆，返回用户信息
        /// </summary>
        /// <param name="qc"></param>
        /// <returns></returns>
        public ResponseResultBase GetToken(TokenGetPO qc)
        {
            var response = new ResponseResultBase();
            var user = _userRepository.Find(x => x.Account == qc.Account && x.Password == qc.Password);
            if (user == null)
            {
                response.SetFailed("用户不存在",ErrorCode.Failed);
                return response;
            }
            else if (user.IsDisabled)
            {
                response.SetFailed("账号已被禁用", ErrorCode.Failed);
                return response;
            }
            var menuList = new List<string>();
            var roleList = user.UserRoleList.Select(x => x.Role).ToList();
            if (roleList != null && roleList.Count > 0)
            {
                menuList = roleList.SelectMany(x => x.RoleMenuList.Select(t => t.Menu.Url)).ToList();
            }
           
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("account", user.Account),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("isAdmin", user.IsAdmin.ToString()),
                    new Claim("avatar", user.Avatar),
                    new Claim("menus",JsonConvert.SerializeObject(menuList))
                });
            var token = this.GenerateToken(claimsIdentity);
            response.Data = token;
            return response;
        }

        /// <summary>
        /// 根绝JWT参数生成token
        /// </summary>
        /// <param name="jwtSettings"></param>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        private string GenerateToken(ClaimsIdentity claimsIdentity)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSetting.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(_jwtSetting.ExpireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
        #endregion
    }
}
