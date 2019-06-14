using System.Collections.Generic;
using System.Threading.Tasks;
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
        Task<List<ParkInfoResponse>> GetParkList(List<string> parkIndexCodes);
        /// <summary>
        /// 停车场唯一标识集
        /// </summary>
        /// <param name="parkIndexCodes"></param>
        /// <returns></returns>
        Task<List<EntranceInfoResponse>> GetEntranceList(List<string> parkIndexCodes);
        /// <summary>
        /// 根据出入口 获取车道列表
        /// </summary>
        /// <param name="entranceIndexCodes">出入口唯一标识集 必填</param>
        /// <returns></returns>
        Task<List<RoadwayInfoResponse>> GetRoadWayList(List<string> entranceIndexCodes);
    }
}