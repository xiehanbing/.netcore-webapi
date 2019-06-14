using System;

namespace General.Api.Application.Parking.Dto
{
    /// <summary>
    /// 停车库列表
    /// </summary>
    public class ParkInfoListResponse
    {
        /// <summary>
        /// 停车场唯一标识
        /// </summary>
        public string  ParkIndexCode { get; set; }
        /// <summary>
        /// 父停车场唯一标识
        /// </summary>
        public string  ParentParkIndexCode { get; set; }
        /// <summary>
        /// 停车场名称
        /// </summary>
        public string  ParkName { get; set; }
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