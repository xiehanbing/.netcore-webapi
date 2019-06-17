using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking;
using General.Api.Application.Parking.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// 停车管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ParkController : ControllerBase
    {
        private readonly IParkingManageService _parkingManageService;
        private readonly IParkRecordService _parkRecordService;
        private readonly IParkingDeviceService _deviceService;
        /// <summary>
        /// construct
        /// </summary>
        /// <param name="parkingManageService"></param>
        /// <param name="parkRecordService"></param>
        /// <param name="deviceService"></param>
        public ParkController(IParkingManageService parkingManageService, IParkRecordService parkRecordService,
            IParkingDeviceService deviceService)
        {
            _parkingManageService = parkingManageService;
            _parkRecordService = parkRecordService;
            _deviceService = deviceService;
        }


        #region parkmanage
        /// <summary>
        /// 获取停车库列表
        /// </summary>
        /// <param name="parkIndexCodes">停车库唯一标识集合</param>
        /// <returns></returns>
        [Route("list"), HttpPost]
        public async Task<List<ParkInfoListResponse>> GetParkList(List<string> parkIndexCodes)
        {
            return await _parkingManageService.GetParkList(parkIndexCodes);
        }
        /// <summary>
        /// 获取出入口
        /// </summary>
        /// <param name="parkIndexCodes">停车场唯一标识集</param>
        /// <returns></returns>
        [Route("entrance"), HttpPost]
        public async Task<List<EntranceInfoResponse>> GetEntranceList(List<string> parkIndexCodes)
        {
            return await _parkingManageService.GetEntranceList(parkIndexCodes);
        }
        /// <summary>
        /// 根据出入口 获取车道列表
        /// </summary>
        /// <param name="entranceIndexCodes">出入口唯一标识集 必填</param>
        /// <returns></returns>
        [Route("entrance/roadway"), HttpPost]
        public async Task<List<RoadwayInfoResponse>> GetRoadWayList(List<string> entranceIndexCodes)
        {
            return await _parkingManageService.GetRoadWayList(entranceIndexCodes);
        }
        /// <summary>
        /// 查询车位信息
        /// </summary>
        /// <param name="parkSysCode">停车场唯一标识码</param>
        /// <param name="spaceNo">车位号</param>
        /// <param name="pageNo">目标页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        [HttpGet, Route("info")]
        public async Task<ListBaseResponse<ParkInfoResponse>> GetParkInfoList(string parkSysCode, string spaceNo, int pageNo,
            int pageSize)
        {
            return await _parkingManageService.GetParkInfoList(parkSysCode, spaceNo, pageNo, pageSize);
        }
        /// <summary>
        /// 查询停车库剩余车位数
        /// </summary>
        /// <param name="parkSysCode">停车库唯一标识码,不传 则查询全部车库车位剩余数量</param>
        /// <returns></returns>
        [Route("remainSpace"),HttpGet]
        public async Task<List<RemainSpaceNumResponse>> GetRemainSpace(string parkSysCode)
        {
            return await _parkingManageService.GetRemainSpace(parkSysCode);
        }


        #endregion

    }
}