using System;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking.Dto.ChargeBill;
using General.Api.Application.Parking.Dto.Record;
using General.Api.Application.Parking.Request.Record;
using General.Core;
using General.Core.Extension;
using General.Core.HttpClient.Extension;
using HttpUtil;
using Microsoft.Extensions.Configuration;

namespace General.Api.Application.Parking
{
    /// <summary>
    /// 停车场记录
    /// </summary>
    public class ParkRecordService : IParkRecordService
    {
        private readonly IHikHttpUtillib _hikHttp;
        /// <summary>
        /// construct
        /// </summary>
        public ParkRecordService(IHikHttpUtillib hikHttp)
        {
            _hikHttp = hikHttp;
        }
        /// <summary>
        /// <see cref="IParkRecordService.GetTempRecordList(string,string,int,int)"/>
        /// </summary>
        public async Task<ListBaseResponse<TempCarInRecordResponse>> GetTempRecordList(string parkSysCode, string plateNo, int pageNo, int pageSize)
        {
            var data = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<TempCarInRecordResponse>>>(
                "/api/pms/v1/tempCarInRecords/page", new
                {
                    parkSysCode,
                    plateNo,
                    pageNo,
                    pageSize
                });
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IParkRecordService.GetRecordList(QueryParkRecordRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<ParkRecoreResponse>> GetRecordList(QueryParkRecordRequest model)
        {
            var data = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<ParkRecoreResponse>>>("/api/pms/v1/parkingRecord/query", model);
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IParkRecordService.GetCrossRecord(string,string,string,DateTime?,DateTime?,int,int)"/>
        /// </summary>
        public async Task<ListBaseResponse<CrossRecordResponse>> GetCrossRecord(string parkSysCode, string entranceSysCode, string plateNo, DateTime? startTime, DateTime? endTime,
            int pageNo, int pageSize)
        {
            var result = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<CrossRecordResponse>>>(
                "/api/pms/v1/crossRecords/page",
            new
            {
                parkSysCode,
                entranceSysCode,
                plateNo,
                startTime = startTime.GetTimeIosFormatter(),
                endTime = endTime.GetTimeIosFormatter(),
                pageNo,
                pageSize
            });
            return result?.Data;
        }
        /// <summary>
        /// <see cref="IParkRecordService.GetCrossRecordV2(string,string,string,DateTime?,DateTime?,int,int)"/>
        /// </summary>
        public async Task<ListBaseResponse<CrossRecordResponse>> GetCrossRecordV2(string parkSysCode, string entranceSysCode, string plateNo, DateTime? startTime, DateTime? endTime,
            int pageNo, int pageSize)
        {
            var result = await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<CrossRecordResponse>>>(
                "/api/pms/v1/crossRecords/page",
                new
                {
                    parkSyscode = parkSysCode,
                    entranceSyscode = entranceSysCode,
                    plateNo,
                    startTime = startTime.GetTimeIosFormatter(),
                    endTime = endTime.GetTimeIosFormatter(),
                    pageNo,
                    pageSize
                });
            var list = result?.Data;
            if (list?.List?.Count > 0)
            {
                foreach (var item in list.List)
                {
                    var amount = await GetParkAmount(item.ParkSysCode, item.PlateNo, startTime, endTime);
                    if (amount == -1)
                        item.TotalCost = null;
                    else
                    {
                        item.TotalCost = amount;
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取 停车缴费费用
        /// </summary>
        /// <param name="parkSysCode">停车库</param>
        /// <param name="plateNo">车牌号</param>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        private async Task<decimal> GetParkAmount(string parkSysCode, string plateNo, DateTime? starTime, DateTime? endTime)
        {
            int current = 1, size = 500;
            bool success = true;
            decimal totalCost = -1;
            while (success)
            {
                var data = await GetBillList(parkSysCode, plateNo, starTime, endTime, current, size);
                if (data?.List?.Count > 0)
                {
                    totalCost += data.List.Sum(o =>
                    {
                        decimal.TryParse(o.TotalCost, out decimal totalCostDe);
                        return totalCostDe;
                    });
                    current++;
                }
                else
                {
                    success = false;
                }
            }

            return totalCost;
        }
        /// <summary>
        /// <see cref="IParkRecordService.GetBillList(string,string,DateTime?,DateTime?,int,int)"/>
        /// </summary>
        public async Task<ListBaseResponse<ChargeBillResponse>> GetBillList(string parkSysCode, string plateNo, DateTime? starTime, DateTime? endTime, int pageNo, int pageSize)
        {
            return (await _hikHttp.PostAsync<HikVisionResponse<ListBaseResponse<ChargeBillResponse>>>(
                "/api/pms/v1/charge_bill/record/search", new
                {
                    parkSyscode = parkSysCode,
                    plateNo = plateNo,
                    starTime = starTime?.GetTimeIosFormatter(),
                    endTime = endTime?.GetTimeIosFormatter(),
                    pageNo = pageNo,
                    pageSize = pageSize
                }))?.Data;
        }

        /// <summary>
        /// <see cref="IParkRecordService.GetCorssVehicleImage(string,string)"/>
        /// </summary>
        public async Task<string> GetCorssVehicleImage(string aswSysCode, string picUri)
        {
            var data = await _hikHttp.PostHttpWebResponseAsync("/api/pms/v1/crossRecords/page", new
            {
                aswSysCode,
                picUri
            });
            return data?.Headers?.Get("Location");
        }
    }
}