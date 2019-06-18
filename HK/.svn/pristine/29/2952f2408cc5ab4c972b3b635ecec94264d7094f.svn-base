using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking.Dto.AlarmCar;
using General.Core;
using General.Core.HttpClient.Extension;
using Microsoft.Extensions.Configuration;

namespace General.Api.Application.Parking
{
    /// <summary>
    /// 布控管理
    /// </summary>
    public class AlarmCarService : IAlarmCarService
    {
        private readonly string _parkingDeviceApi;
        /// <summary>
        /// construct
        /// </summary>
        public AlarmCarService(IConfiguration configuration)
        {
            _parkingDeviceApi = configuration[HikVisionContext.HikVisionBaseApiName];
            if (string.IsNullOrEmpty(_parkingDeviceApi))
            {
                throw new MyException("parkingDeviceApi is null");
            }
        }
        /// <summary>
        /// <see cref="IAlarmCarService.Addition(string,string,string,string,string,DateTime?)"/>
        /// </summary>
        public async Task<Dto.AlarmCar.AlarmCarAdditionResponse> Addition(string plateNo, string cardNo, string driver, string droberPhone, string remark, DateTime? endTime)
        {
            var data = await _parkingDeviceApi.AppendFormatToHik("/api/pms/v1/alarmCar/addition")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    plateNo,
                    cardNo,
                    driver,
                    droberPhone,
                    remark,
                    endTime
                })
                .ReciveJsonResultAsync<HikVisionResponse<Dto.AlarmCar.AlarmCarAdditionResponse>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IAlarmCarService.Deletion(List{string})"/>
        /// </summary>
        public async Task<bool> Deletion(List<string> alarmSyscodes)
        {
            string syscode = alarmSyscodes?.Count > 0 ? string.Join(",", alarmSyscodes) : "";
            var data = await _parkingDeviceApi.AppendFormatToHik("/api/pms/v1/alarmCar/deletion")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    alarmSyscodes = syscode
                })
                .ReciveJsonResultAsync<HikVisionResponse>();
            return data?.Success ?? false;
        }
        /// <summary>
        /// <see cref="IAlarmCarService.GetList(string,int,int)"/>
        /// </summary>
        public async Task<ListBaseResponse<AlarmCarListResponse>> GetList(string keyNo, int pageNo, int pageSize)
        {
            var data = await _parkingDeviceApi.AppendFormatToHik("/api/pms/v1/alarmCar/page")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    searchKey = keyNo,
                    pageNo,
                    pageSize
                })
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<AlarmCarListResponse>>>();
            return data?.Data;
        }
    }
}