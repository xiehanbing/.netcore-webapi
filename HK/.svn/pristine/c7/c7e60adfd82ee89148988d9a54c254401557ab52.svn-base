using System;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking.Dto.Record;
using General.Api.Application.Parking.Request.Record;
using General.Core;
using General.Core.HttpClient.Extension;
using Microsoft.Extensions.Configuration;

namespace General.Api.Application.Parking
{
    /// <summary>
    /// 停车场记录
    /// </summary>
    public class ParkRecordService : IParkRecordService
    {
        private readonly string _parkingApi;
        /// <summary>
        /// construct
        /// </summary>
        public ParkRecordService(IConfiguration configuration)
        {
            _parkingApi = configuration[HikVisionContext.HikVisionBaseApiName];
            if (string.IsNullOrEmpty(_parkingApi))
            {
                throw new MyException("parkingApi is null");
            }
        }
        /// <summary>
        /// <see cref="IParkRecordService.GetTempRecordList(string,string,int,int)"/>
        /// </summary>
        public async Task<ListBaseResponse<TempCarInRecordResponse>> GetTempRecordList(string parkSysCode, string plateNo, int pageNo, int pageSize)
        {
            var data = await _parkingApi.AppendFormatToHik("/api/pms/v1/tempCarInRecords/page")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    parkSysCode,
                    plateNo,
                    pageNo,
                    pageSize
                })
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<TempCarInRecordResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IParkRecordService.GetRecordList(QueryParkRecordRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<ParkRecoreResponse>> GetRecordList(QueryParkRecordRequest model)
        {
            var data = await _parkingApi.AppendFormatToHik("/api/pms/v1/parkingRecord/query")
                .SetHiKSecreity()
                .PostAsync(model)
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<ParkRecoreResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IParkRecordService.GetCrossRecord(string,string,string,DateTime?,DateTime?,int,int)"/>
        /// </summary>
        public async Task<ListBaseResponse<CrossRecordResponse>> GetCrossRecord(string parkSysCode, string entranceSysCode, string plateNo, DateTime? startTime, DateTime? endTime,
            int pageNo, int pageSize)
        {
            var data = await _parkingApi.AppendFormatToHik("/api/pms/v1/crossRecords/page")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    parkSysCode,
                    entranceSysCode,
                    plateNo,
                    startTime,
                    endTime,
                    pageNo,
                    pageSize
                })
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<CrossRecordResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IParkRecordService.GetCorssVehicleImage(string,string)"/>
        /// </summary>
        public async Task<string> GetCorssVehicleImage(string aswSysCode, string picUri)
        {
            var data = await _parkingApi.AppendFormatToHik("/api/pms/v1/crossRecords/page")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    aswSysCode,
                    picUri
                })
                .ReciveResponseHeadersByKey("Location");
            return data?.FirstOrDefault();
        }
    }
}