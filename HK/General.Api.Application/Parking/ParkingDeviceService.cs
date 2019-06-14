using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking.Dto.Device;
using General.Api.Application.Parking.Request.Device;
using General.Core;
using General.Core.HttpClient.Extension;
using Microsoft.Extensions.Configuration;

namespace General.Api.Application.Parking
{
    /// <summary>
    /// 停车场设备服务
    /// </summary>
    public class ParkingDeviceService : IParkingDeviceService
    {
        private readonly string _parkingDeviceApi;
        /// <summary>
        /// construct
        /// </summary>
        public ParkingDeviceService(IConfiguration configuration)
        {
            _parkingDeviceApi = configuration[HikVisionContext.HikVisionBaseApiName];
            if (string.IsNullOrEmpty(_parkingDeviceApi))
            {
                throw new MyException("parkingDeviceApi is null");
            }
        }
        /// <summary>
        /// <see cref="IParkingDeviceService.DoControl(Request.Device.DeviceControlRequest)"/>
        /// </summary>
        public async Task<ListBaseResponse<DeviceControlResponse>> DoControl(Request.Device.DeviceControlRequest model)
        {
            var data = await _parkingDeviceApi.AppendFormatToHik("/api/pms/v1/deviceControl")
                .SetHiKSecreity()
                .PostAsync(model)
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<DeviceControlResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="IParkingDeviceService.DoControlBatch(string,DeviceCommandType)"/>
        /// </summary>
        public async Task<bool> DoControlBatch(string parkSyscode, DeviceCommandType command)
        {
            var data = await _parkingDeviceApi.AppendFormatToHik("/api/pms/v1/deviceControl")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    parkSyscode,
                    command = command.GetHashCode()
                })
                .ReciveJsonResultAsync<HikVisionResponse>();
            return data?.Success ?? false;
        }
    }
}