using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking.Dto;

namespace General.Api.Application.Parking
{
    /// <summary>
    /// 停车场管理服务
    /// </summary>
    public interface IParkingManageService
    {
        /// <summary>
        /// 获取停车库列表
        /// </summary>
        /// <param name="parkIndexCodes">停车库唯一标识集合</param>
        /// <returns></returns>
        Task<List<ParkInfoListResponse>> GetParkList(List<string> parkIndexCodes);
        /// <summary>
        /// 获取出入口
        /// </summary>
        /// <param name="parkIndexCodes">停车场唯一标识集</param>
        /// <returns></returns>
        Task<List<EntranceInfoResponse>> GetEntranceList(List<string> parkIndexCodes);
        /// <summary>
        /// 根据出入口 获取车道列表
        /// </summary>
        /// <param name="entranceIndexCodes">出入口唯一标识集 必填</param>
        /// <returns></returns>
        Task<List<RoadwayInfoResponse>> GetRoadWayList(List<string> entranceIndexCodes);

        /// <summary>
        /// 查询车位信息
        /// </summary>
        /// <param name="parkSysCode">停车场唯一标识码</param>
        /// <param name="spaceNo">车位号</param>
        /// <param name="pageNo">目标页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        Task<ListBaseResponse<Dto.ParkInfoResponse>> GetParkInfoList(string parkSysCode, string spaceNo, int pageNo,
            int pageSize);
        /// <summary>
        /// 查询停车库剩余车位数
        /// </summary>
        /// <param name="parkSysCode">停车库唯一标识码,不传 则查询全部车库车位剩余数量</param>
        /// <returns></returns>
        Task<List<RemainSpaceNumResponse>> GetRemainSpace(string parkSysCode);
    }
}