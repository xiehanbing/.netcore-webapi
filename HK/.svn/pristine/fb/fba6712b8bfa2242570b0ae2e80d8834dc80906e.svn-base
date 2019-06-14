using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.User.Dto;
using General.Api.Application.User.Request;

namespace General.Api.Application.User
{
    /// <summary>
    /// 人员信息接口
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// GetList
        /// </summary>
        /// <returns></returns>
        Task<List<Dto.UserDto>> GetList();
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="query">参数</param>
        /// <returns></returns>
        Task<ListBaseResponse<UserResponse>> GetUserList(UserQuery query);


    }
}