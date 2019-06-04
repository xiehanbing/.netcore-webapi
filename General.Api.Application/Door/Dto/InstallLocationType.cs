using System.ComponentModel;

namespace General.Api.Application.Door.Dto
{
    /// <summary>
    /// 安装位置类型
    /// </summary>
    public enum InstallLocationType
    {
        /// <summary>
        /// 小区周界
        /// </summary>
        [Description("小区周界")]
        CommunityPerimeter=1,
        /// <summary>
        /// 小区出入口
        /// </summary>
        [Description("小区出入口")]
        CommunityEntrance =2,
        /// <summary>
        /// 消防通道
        /// </summary>
        [Description("消防通道")]
        FireChannel =3,
        /// <summary>
        /// 景观池
        /// </summary>
        [Description("景观池")]
        LandscapePool =4,
        /// <summary>
        /// 住宅楼外
        /// </summary>
        [Description("住宅楼外")]
        OutsideBuilding =5,
        /// <summary>
        /// 停车场（库）出入口
        /// </summary>
        [Description("停车场（库）出入口")]
        ParkEntrance =6,
        /// <summary>
        /// 停车场区
        /// </summary>
        [Description("停车场区")]
        ParkArea =7,
        /// <summary>
        /// 设备房（机房、配电房、泵房）
        /// </summary>
        [Description("设备房（机房、配电房、泵房）")]
        EquipmentRoom =8,
        /// <summary>
        /// 监控中心
        /// </summary>
        [Description("监控中心")]
        MonitorCenter =9,
        /// <summary>
        /// 禁停区
        /// </summary>
        [Description("禁停区")]
        StopArea =10,
        /// <summary>
        /// 金库
        /// </summary>
        [Description("金库")]
        Vault=11
    }
}