using System;

namespace General.Api.Application.Parking.Dto.AlarmCar
{
    /// <summary>
    /// 布控车辆列表
    /// </summary>
    public class AlarmCarListResponse
    {
        /// <summary>
        /// 布控唯一标识
        /// </summary>
        public string  AlarmSysCode { get; set; }
        /// <summary>
        ///  	车牌号码
        /// </summary>
        public string  PlateNo { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string  CardNo { get; set; }
        /// <summary>
        /// 驾驶员名称
        /// </summary>
        public string  DriverName { get; set; }
        /// <summary>
        /// 驾驶员电话
        /// </summary>
        public string  DriverPhone { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string  Remark { get; set; }
        /// <summary>
        /// 布控结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}