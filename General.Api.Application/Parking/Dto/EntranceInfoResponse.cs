using System;

namespace General.Api.Application.Parking.Dto
{
    /// <summary>
    /// 出入口信息结构体
    /// </summary>
    public class EntranceInfoResponse
    {
        /// <summary>
        /// 出入口唯一标识
        /// </summary>
        public string EntranceIndexCode { get; set; }
        /// <summary>
        /// 出入口名称
        /// </summary>
        public string EntranceName { get; set; }
        /// <summary>
        /// 停车场唯一标识
        /// </summary>
        public string ParkIndexCode { get; set; }
        /// <summary>
        /// 车道数
        /// </summary>
        public int RoadNum { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}