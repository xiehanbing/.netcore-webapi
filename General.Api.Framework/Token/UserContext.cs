using System.Collections.Generic;
using System.Linq;

namespace General.Api.Framework.Token
{
    public class UserContext
    {
        /// <summary>
        /// 允许所有人访问的接口地址
        /// </summary>
        public static List<string> AllowAnonymousPathList = new List<string>();
        /// <summary>
        /// 用户权限列表
        /// </summary>
        public static List<string> UserAuthList { get; set; }
        public bool IsAllowAnonymous(string urlPath)
        {
            if (urlPath.ToLower().Contains("swagger"))
                return true;
            if (AllowAnonymousPathList.Contains(urlPath))
                return true;
            return false;
        }

        public void TryInit(string user)
        {
            //todo init user auth list 
        }

        public bool Authorize(string path)
        {
            return true;
            //这里暂就不做校验是否有权限
            if (UserAuthList.Any(o => o.ToLower().Equals(path.ToLower()))) return true;
            //获取用户的权限列表进行验证
            //todo author
            return false;
        }
    }
}