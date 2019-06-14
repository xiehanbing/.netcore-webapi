using System.Collections.Generic;
using System.ComponentModel;

namespace General.Api.Application.Door.Dto
{
    /// <summary>
    /// 资源信息
    /// </summary>
    public class ResourceInfo
    {
        /// <summary>
        /// 资源的唯一标识
        /// </summary>
        public string  ResourceIndexCode { get; set; }
        /// <summary>
        /// 资源类型 acsDevice:门禁设备；ecsDevice:梯控设备；visDevice:可视对讲设备
        /// </summary>
        public ResourceType ResourceType { get; set; }
        /// <summary>
        /// 最多支持128个通道下载；表示此次数据需要下载到设备的哪些通道上；可以通过【获取资源列表】返回参数获取到相应资源的通道号；门禁设备通道指门禁点；梯控设备通道指楼层；可视对讲设备无通道
        /// </summary>
        public List<int> ChannelNos { get; set; }

    }
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// 门禁设备
        /// </summary>
        [Description("门禁设备")]
        AcsDevice=1,
        /// <summary>
        /// 梯控设备
        /// </summary>
        [Description("梯控设备")]
        EcsDevice =2,
        /// <summary>
        /// 可视对讲设备
        /// </summary>
        [Description("可视对讲设备")]
        VisDevice =3
    }

}