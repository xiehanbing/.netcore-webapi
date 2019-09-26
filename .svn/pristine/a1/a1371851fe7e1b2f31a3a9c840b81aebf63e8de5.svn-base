using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking.Dto.Device;
using General.Api.Application.Parking.Request.Device;

namespace General.Api.Application.Parking
{
    /// <summary>
    /// 停车场设备服务
    /// </summary>
    public interface IParkingDeviceService
    {
        /// <summary>
        /// 道闸反控
        /// </summary>
        /// <param name="model">请求类</param>
        /// <returns></returns>
        Task<ListBaseResponse<DeviceControlResponse>> DoControl(Request.Device.DeviceControlRequest model);
        /// <summary>
        /// 根据停车场编码反控道闸 
        /// </summary>
        /// <param name="parkSyscode">停车场唯一标识码</param>
        /// <param name="command">控闸命令</param>
        /// <returns></returns>
        Task<bool> DoControlBatch(string parkSyscode, DeviceCommandType command);
    }
}