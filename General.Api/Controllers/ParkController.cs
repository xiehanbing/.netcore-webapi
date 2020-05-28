﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking;
using General.Api.Application.Parking.Dto;
using General.Api.Application.Parking.Dto.Device;
using General.Api.Application.Parking.Dto.Record;
using General.Api.Application.Parking.Request.Device;
using General.Api.Framework.Filters;
using General.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlMatches;

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
        /// 获取停车库列表
        /// </summary>
        /// <param name="parkIndexCodes">停车库唯一标识集合</param>
        /// <returns></returns>
        [Route("list/test"), HttpPost, SwaggerIgnore(true)]
        public async Task<List<ParkInfoListResponse>> GetParkTestList(List<string> parkIndexCodes)
        {
            List<ParkInfoListResponse> list = new List<ParkInfoListResponse>();
            for (int i = 0; i < 50; i++)
            {
                list.Add(new ParkInfoListResponse()
                {
                    ParkName = "测试" + i,
                    ParkIndexCode = i.ToString()
                });
            }

            return list;
            // return await _parkingManageService.GetParkList(parkIndexCodes);
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
        [Route("remainSpace"), HttpGet]
        public async Task<List<RemainSpaceNumResponse>> GetRemainSpace(string parkSysCode)
        {
            return await _parkingManageService.GetRemainSpace(parkSysCode);
        }


        #endregion


        #region park device
        /// <summary>
        /// 道闸反控
        /// </summary>
        /// <param name="model">请求类</param>
        /// <returns></returns>
        [HttpPost, Route("device/control")]
        public async Task<ListBaseResponse<DeviceControlResponse>> DoControl(DeviceControlRequest model)
        {
            return await _deviceService.DoControl(model);
        }
        /// <summary>
        /// 根据停车场编码反控道闸
        /// </summary>
        /// <param name="parkSyscode">停车场唯一标识码</param>
        /// <param name="command">控闸命令 0关闸 1开闸 3常开</param>
        /// <returns></returns>
        [HttpGet, Route("device/control/byParkCode")]
        public async Task<bool> DoControlBatch([Required]string parkSyscode, DeviceCommandType command)
        {
            return await _deviceService.DoControlBatch(parkSyscode, command);
        }


        #endregion


        #region park record
        /// <summary>
        /// 查询场内车辆停车信息
        /// </summary>
        /// <param name="parkSysCode">停车库唯一标识码</param>
        /// <param name="plateNo">车牌号码</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns></returns>
        [HttpGet, Route("record/temp")]
        public async Task<ListBaseResponse<TempCarInRecordResponse>> GetTempRecordList(string parkSysCode,
            string plateNo,
            [RegularExpression(ApiConsts.MoreThanZeroRegex, ErrorMessage = "pageNo more than zero")]int pageNo, [RegularExpression(ApiConsts.MoreThanZeroRegex, ErrorMessage = "pageSize more than zero")]int pageSize)
        {
            //if (!ModelState.IsValid)
            //{
            //    throw new ValidatorException("pageno or pagesize is not null");
            //}
            return await _parkRecordService.GetTempRecordList(parkSysCode, plateNo, pageNo, pageSize);
        }
        /// <summary>
        /// 查询过车记录
        /// </summary>
        /// <param name="parkSysCode">停车库唯一标识</param>
        /// <param name="entranceSysCode">出入口唯一标识</param>
        /// <param name="plateNo">车牌号</param>
        /// <param name="startTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="pageNo">目标页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        [HttpGet, Route("record/cross")]
        public async Task<ListBaseResponse<CrossRecordResponse>> GetCrossRecord(string parkSysCode,
            string entranceSysCode, string plateNo, DateTime? startTime, DateTime? endTime, int pageNo, int pageSize)
        {
            return await _parkRecordService.GetCrossRecord(parkSysCode, entranceSysCode, plateNo, startTime, endTime,
                pageNo, pageSize);
        }


        /// <summary>
        /// 查询过车记录
        /// </summary>
        /// <param name="parkSysCode">停车库唯一标识</param>
        /// <param name="entranceSysCode">出入口唯一标识</param>
        /// <param name="plateNo">车牌号</param>
        /// <param name="startTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="pageNo">目标页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        [HttpGet, Route("record/cross/v2")]
        public async Task<ListBaseResponse<CrossRecordResponse>> GetCrossRecordV2(string parkSysCode,
            string entranceSysCode, string plateNo, DateTime? startTime, DateTime? endTime, int pageNo, int pageSize)
        {
            return await _parkRecordService.GetCrossRecordV2(parkSysCode, entranceSysCode, plateNo, startTime, endTime,
                pageNo, pageSize);
        }


        /// <summary>
        /// 查询过车记录
        /// </summary>
        /// <param name="parkSysCode">停车库唯一标识</param>
        /// <param name="entranceSysCode">出入口唯一标识</param>
        /// <param name="plateNo">车牌号</param>
        /// <param name="startTime">查询开始时间</param>
        /// <param name="endTime">查询结束时间</param>
        /// <param name="pageNo">目标页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        [HttpGet, Route("record/cross/v2/test"), SwaggerIgnore(true)]
        public async Task<ListBaseResponse<CrossRecordResponse>> GetCrossRecordV2Test(string parkSysCode,
            string entranceSysCode, string plateNo, DateTime? startTime, DateTime? endTime, int pageNo, int pageSize)
        {
            Random random = new Random();
            ListBaseResponse<CrossRecordResponse> list = new ListBaseResponse<CrossRecordResponse>();
            list.List = new List<CrossRecordResponse>();
            list.Total = 100;
            int start = (pageNo - 1) * pageSize + 1;
            int end = pageNo * pageSize;
            if (end > list.Total)
                end = list.Total;
            for (int index = start; index <= end; index++)
            {
                int randomValue = index % 2;
                list.List.Add(new CrossRecordResponse()
                {
                    ParkSysName = "测试" + index,
                    PlateNo = "浙A1234" + index,
                    CrossTime = DateTime.Now,
                    VehicleOut = randomValue,
                    TotalCost = randomValue > 0 ? random.Next(0, 100) : -1
                });
            }
            return list;
        }
        #endregion
    }
}