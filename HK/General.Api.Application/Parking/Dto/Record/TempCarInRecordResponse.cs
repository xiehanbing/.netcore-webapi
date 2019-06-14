using System;

namespace General.Api.Application.Parking.Dto.Record
{
    /// <summary>
    /// 场内车辆停车信息
    /// </summary>
    public class TempCarInRecordResponse
    {
        /// <summary>
        /// 停车信息唯一标识
        /// </summary>
        public string InRecordSyscode { get; set; }
        /// <summary>
        /// 车辆图片uri
        /// </summary>
        public string VehiclePicUri { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string  CardNo { get; set; }
        /// <summary>
        /// 入场时间
        /// </summary>
        public DateTime? InTime { get; set; }
        /// <summary>
        /// 停车时长
        /// </summary>
        public string ParkTime { get; set; }
        /// <summary>
        /// 车辆所在停车库唯一标识
        /// </summary>
        public string  ParkSysCode { get; set; }
        /// <summary>
        /// 车辆所在停车库名称
        /// </summary>
        public string  ParkName { get; set; }
        /// <summary>
        /// 车牌照片uri
        /// </summary>
        public string  PlateNoPicUri { get; set; }
        /// <summary>
        /// 图片服务唯一标识
        /// </summary>
        public string  AswSysCode { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string  PlateNo { get; set; }
    }
}