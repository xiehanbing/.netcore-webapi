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
        /// <summary>
        /// 获取所有人员列表
        /// </summary>
        /// <returns></returns>
        Task<List<UserResponse>> GetAllUser();
        /// <summary>
        /// 获取人员详情
        /// </summary>
        /// <param name="personId">人员ID</param>
        /// <returns></returns>
        Task<UserResponse> GetDetail(string personId);

        //Task<UserResponse> GetDetail(string personId);
        /// <summary>
        /// 批量获取人员详情
        /// </summary>
        /// <param name="personIds">人员ID</param>
        /// <returns></returns>
        Task<List<UserResponse>> GetDetail(List<string> personIds);

        /// <summary>
        /// 批量获取人员详情
        /// </summary>
        /// <param name="personIds">人员ID</param>
        /// <returns></returns>
        Task<List<UserResponse>> GetDetailV2(List<string> personIds);

    }
}