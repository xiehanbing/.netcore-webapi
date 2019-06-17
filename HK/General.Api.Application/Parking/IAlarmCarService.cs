using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Parking.Dto.AlarmCar;

namespace General.Api.Application.Parking
{
    /// <summary>
    /// 车辆布控管理
    /// </summary>
    public interface IAlarmCarService
    {
        /// <summary>
        /// 车辆布控
        /// </summary>
        /// <param name="plateNo">车牌号码</param>
        /// <param name="cardNo">卡号</param>
        /// <param name="driver">驾驶员名称</param>
        /// <param name="droberPhone">驾驶员电话</param>
        /// <param name="remark">备注信息</param>
        /// <param name="endTime">布控结束时间</param>
        /// <returns></returns>
        Task<Dto.AlarmCar.AlarmCarAdditionResponse> Addition(string plateNo, string cardNo, string driver, string droberPhone, string remark,
            DateTime? endTime);
        /// <summary>
        /// 取消布控
        /// </summary>
        /// <param name="alarmSyscodes">布控车辆唯一标识集合</param>
        /// <returns></returns>
        Task<bool> Deletion(List<string> alarmSyscodes);
        /// <summary>
        /// 查询布控车辆
        /// </summary>
        /// <param name="keyNo">车牌号/卡号</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">记录数</param>
        /// <returns></returns>
        Task<ListBaseResponse<AlarmCarListResponse>> GetList(string keyNo, int pageNo, int pageSize);
    }
}