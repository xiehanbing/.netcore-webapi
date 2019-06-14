using General.Api.Application.Token;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Core;

namespace General.Api.Framework.Token
{
    /// <summary>
    /// UserContext
    /// </summary>
    public class UserContext
    {
        private readonly ITokenService _tokenService;
        /// <summary>
        /// UserContext
        /// </summary>
        /// <param name="tokenService"></param>
        public UserContext(ITokenService tokenService)
        {
            _tokenService = tokenService;
        } 
        /// <summary>
        /// 允许所有人访问的接口地址
        /// </summary>
        public static List<string> AllowAnonymousPathList = new List<string>();
        /// <summary>
        /// 用户权限列表
        /// </summary>
        public static List<string> UserAuthList { get; set; }
        /// <summary>
        /// IsAllowAnonymous
        /// </summary>
        /// <param name="urlPath">urlPath</param>
        /// <returns></returns>
        public bool IsAllowAnonymous(string urlPath)
        {
            if (urlPath.ToLower().Contains("swagger"))
                return true;
            if (AllowAnonymousPathList.Contains(urlPath))
                return true;
            return false;
        }
        /// <summary>
        /// init user permission
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void TryInit(string user)
        {
            if (UserAuthList == null)
            {
                var data = _tokenService.GetPermission(user).ConfigureAwait(false).GetAwaiter().GetResult();
                UserAuthList = data;
            }
            //todo init user auth list 
        }
        /// <summary>
        /// Authorize
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="path">path</param>
        /// <returns></returns>
        public bool Authorize(string user, string path)
        {
            //return true;
            if (UserAuthList == null) TryInit(user);
            if (UserAuthList?.Exists(o => o.Equals("All")) ?? false) return true;

            var data = _tokenService.ValidatePermission(user, path).ConfigureAwait(false).GetAwaiter().GetResult();
            return data;
            //这里暂就不做校验是否有权限
            if (UserAuthList.Any(o => o.ToLower().Equals(path.ToLower()))) return true;
            //获取用户的权限列表进行验证
            //todo author
            return false;
        }
    }
}