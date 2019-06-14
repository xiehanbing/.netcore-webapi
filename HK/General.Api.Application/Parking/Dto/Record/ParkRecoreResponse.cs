using System;

namespace General.Api.Application.Parking.Dto.Record
{
    /// <summary>
    /// 停车场记录
    /// </summary>
    public class ParkRecoreResponse
    {
        /// <summary>
        /// 停车记录编号
        /// </summary>
        public string  RecordSysCode { get; set; }
        /// <summary>
        /// 车位编号
        /// </summary>
        public string  SpaceSysCode { get; set; }
        /// <summary>
        /// 车位号
        /// </summary>
        public string  SpaceNo { get; set; }
        /// <summary>
        /// 车位图片uri
        /// </summary>
        public string  SpacePicUri { get; set; }
        /// <summary>
        /// 停车时间
        /// </summary>
        public DateTime? ParkingTime { get; set; }
        /// <summary>
        /// 车辆所在停车库唯一标识
        /// </summary>
        public string  ParkSysCode { get; set; }
        /// <summary>
        /// 车辆所在停车库名称
        /// </summary>
        public string  ParkName { get; set; }
        /// <summary>
        /// 车辆所在楼层唯一标识
        /// </summary>
        public string  FloorSysCode { get; set; }
        /// <summary>
        /// 车辆所在楼层名称
        /// </summary>
        public string  FloorName { get; set; }
        /// <summary>
        /// 车牌照片uri
        /// </summary>
        public string  PlateNoPicUri { get; set; }
        /// <summary>
        /// 图片服务唯一标识
        /// </summary>
        public string  AswSysCode { get; set; }
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string  PlateNo { get; set; }
    }
}