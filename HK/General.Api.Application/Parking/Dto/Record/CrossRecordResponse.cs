using System;

namespace General.Api.Application.Parking.Dto.Record
{
    /// <summary>
    /// 过车记录
    /// </summary>
    public class CrossRecordResponse
    {
        /// <summary>
        /// 过车记录唯一标识
        /// </summary>
        public string  CrossRecordSysCode { get; set; }
        /// <summary>
        /// 停车库唯一标识
        /// </summary>
        public string  ParkSysCode { get; set; }
        /// <summary>
        /// 停车库名称
        /// </summary>
        public string  ParkSysName { get; set; }
        /// <summary>
        /// 出入口唯一标识
        /// </summary>
        public string  EntranceSysCode { get; set; }
        /// <summary>
        /// 出入口名称
        /// </summary>
        public string  EntranceName { get; set; }
        /// <summary>
        /// 是否出场 0-进场， 1-出场
        /// </summary>
        public int VehicleOut { get; set; }
        /// <summary>
        /// 放行模式 0-禁止放行，1-固定车包期，2-临时车入场，3-预约车入场，10-离线出场，11-缴费出场，12-预付费出场，13-免费出场，30- 非法卡不放行，31-手动放行，32-特殊车辆放行，33-节假日放行，35-群组放行，36-遥控器开闸
        /// </summary>
        public int? ReleaseMode { get; set; }
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string  PlateNo { get; set; }
        /// <summary>
        /// 卡片号码
        /// </summary>
        public string  CardNo { get; set; }
        /// <summary>
        /// 车辆颜色 0：其他颜色； 1：白色； 2：银色； 3：灰色； 4：黑色； 5：红色； 6：深蓝色； 7：蓝色； 8：黄色； 9：绿色； 10：棕色； 11：粉色； 12：紫色’
        /// </summary>
        public int?  VehicleColor { get; set; }
        /// <summary>
        /// 车辆类型 0：其他车； 1：小型车； 2：大型车； 3：摩托车
        /// </summary>
        public int?  VehicleType { get; set; }
        /// <summary>
        /// 车辆图片uri
        /// </summary>
        public string  VehiclePicUri { get; set; }
        /// <summary>
        /// 车牌图片uri
        /// </summary>
        public string  PlateNoPicUri { get; set; }
        /// <summary>
        /// 人脸图片uri
        /// </summary>
        public string  FacePicUri { get; set; }
        /// <summary>
        /// 图片服务唯一标识
        /// </summary>
        public string  AswSysCode { get; set; }
        /// <summary>
        /// 通过时间
        /// </summary>
        public DateTime? CrossTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}