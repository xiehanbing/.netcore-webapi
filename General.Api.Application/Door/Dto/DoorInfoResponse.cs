using System;
using General.Core.Extension;

namespace General.Api.Application.Door.Dto
{
    /// <summary>
    /// 门禁信息
    /// </summary>
    public class DoorInfoResponse
    {
        /// <summary>
        /// 门禁点唯一标识
        /// </summary>
        public string DoorIndexCode { get; set; }
        /// <summary>
        /// 门禁点名称
        /// </summary>
        public string DoorName { get; set; }
        /// <summary>
        /// 门禁点序号
        /// </summary>
        public string DoorNo { get; set; }
        /// <summary>
        /// 所属门禁设备唯一标示
        /// </summary>
        public string AcsDevIndexCode { get; set; }
        /// <summary>
        /// 所属区域唯一标示
        /// </summary>
        public string RegionIndexCode { get; set; }
        /// <summary>
        /// 通道类型
        /// </summary>
        public string ChannelType { get; set; }
        /// <summary>
        /// 通道号
        /// </summary>
        public string ChannelNo { get; set; }
        /// <summary>
        /// 安装位置
        /// </summary>
        public InstallLocationType? InstallLocation { get; set; }
        /// <summary>
        /// 安装位置描述
        /// </summary>
        public string InstallLocationDesc =>
            typeof(InstallLocationType).GetEnumDescription(InstallLocation?.GetHashCode()??-1);
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
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