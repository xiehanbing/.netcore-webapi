using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking.Dto;
using General.Core;
using General.Core.Extension;
using General.Core.HttpClient.Extension;
using HttpUtil;
using Microsoft.Extensions.Configuration;

namespace General.Api.Application.Parking
{
    /// <summary>
    /// 停车场管理服务
    /// </summary>
    public class ParkingManageService : IParkingManageService
    {
        private readonly string _parkingApi;
        private readonly IHikHttpUtillib _hikHttp;
        /// <summary>
        /// construct
        /// </summary>
        public ParkingManageService(IConfiguration configuration, IHikHttpUtillib hikHttp)
        {
            _hikHttp = hikHttp;
            //_parkingApi = configuration[HikVisionContext.HikVisionBaseApiName];
            //if (string.IsNullOrEmpty(_parkingApi))
            //{
            //    throw new MyException("parkingApi is null");
            //}
        }
        /// <summary>
        /// <see cref="IParkingManageService.GetParkList(List{string})"/>
        /// </summary>
        public async Task<List<ParkInfoListResponse>> GetParkList(List<string> parkIndexCodes)
        {
            var data = await _hikHttp.PostAsync<HikVisionResponse<List<ParkInfoListResponse>>>("/api/resource/v1/park/parkList", new
            {
                parkIndexCodes = (parkIndexCodes?.Any() ?? false) ? (string.Join(",", parkIndexCodes)) : null
            });
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IParkingManageService.GetEntranceList(List{string})"/>
        /// </summary>
        public async Task<List<EntranceInfoResponse>> GetEntranceList(List<string> parkIndexCodes)
        {
            var data = await _parkingApi.AppendFormatToHik("/api/resource/v1/roadway/roadwayList")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    parkIndexCodes = parkIndexCodes.StringJoin(",")
                })
                .ReciveJsonResultAsync<HikVisionResponse<List<EntranceInfoResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IParkingManageService.GetRoadWayList(List{string})"/>
        /// </summary>
        public async Task<List<RoadwayInfoResponse>> GetRoadWayList(List<string> entranceIndexCodes)
        {
            var data = await _parkingApi.AppendFormatToHik("/api/resource/v1/roadway/roadwayList")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    entranceIndexCodes = entranceIndexCodes.StringJoin(",")
                })
                .ReciveJsonResultAsync<HikVisionResponse<List<RoadwayInfoResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IParkingManageService.GetParkInfoList(string,string,int,int)"/>
        /// </summary>
        public async Task<ListBaseResponse<Dto.ParkInfoResponse>> GetParkInfoList(string parkSysCode, string spaceNo, int pageNo, int pageSize)
        {
            var data = await _parkingApi.AppendFormatToHik("/api/pms/v1/parkingSpace/spaceNo")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    parkSyscode = parkSysCode,
                    spaceNo,
                    pageNo,
                    pageSize
                })
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<ParkInfoResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IParkingManageService.GetRemainSpace(string)"/>
        /// </summary>
        public async Task<List<RemainSpaceNumResponse>> GetRemainSpace(string parkSysCode)
        {
            var data = await _parkingApi.AppendFormatToHik("/api/pms/v1/parkingSpace/spaceNo")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    parkSyscode = parkSysCode
                })
                .ReciveJsonResultAsync<HikVisionResponse<List<RemainSpaceNumResponse>>>();
            return data?.Data;
        }
    }
}