using System.Threading.Tasks;
using General.Api.Application.Hikvision;

namespace General.Api.Application.Organize
{
    /// <summary>
    /// 组织
    /// </summary>
    public interface IOrganizeService
    {
        /// <summary>
        /// 获取所有组织列表
        /// </summary>
        /// <returns></returns>
        Task<ListBaseResponse<OrganizeInfoResponse>> GetAll();
        /// <summary>
        /// 获取所有组织列表
        /// </summary>
        /// <returns></returns>
        Task<ListBaseResponse<OrganizeInfoResponse>> GetList(OrganizeRequest request);
    }
}