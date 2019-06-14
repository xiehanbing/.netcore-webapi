using System.ComponentModel;

namespace General.Api.Application.Hikvision.Resource
{
    /// <summary>
    /// 入侵报警服务事件类型
    /// </summary>
    public enum EventWarningType
    {
        /// <summary>
        /// 防区报警
        /// </summary>
        [Description("防区报警")]
        FangQuBaoJing= 327681,
        /// <summary>
        /// 布防
        /// </summary>
        [Description("布防")]
        BuFang =327937,
        /// <summary>
        /// 撤防
        /// </summary>
        [Description("撤防")]
        CheFang =327938,
        /// <summary>
        /// 旁路
        /// </summary>
        [Description("旁路")]
        PangLu =327939,
        /// <summary>
        /// 旁路恢复
        /// </summary>
        [Description("旁路恢复")]
        PangLuHuiFu =327940,
        /// <summary>
        /// 消警
        /// </summary>
        [Description("消警")]
        XiaoJing =327941
    }
}