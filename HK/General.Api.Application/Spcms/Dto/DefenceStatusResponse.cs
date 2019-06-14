using System.ComponentModel;

namespace General.Api.Application.Spcms.Dto
{
    /// <summary>
    /// 防区状态
    /// </summary>
    public class DefenceStatusResponse
    {
        /// <summary>
        /// 防区编码
        /// </summary>
        public string  DefenceIndexCode { get; set; }
        /// <summary>
        /// 防区状态
        /// </summary>
        public DefenceStatusType Status { get; set; }
    }
    /// <summary>
    /// 子系统状态
    /// </summary>
    public enum DefenceStatusType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnow = -1,
        /// <summary>
        /// 离线
        /// </summary>
        [Description("离线")]
        Leave = 0,
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 故障
        /// </summary>
        [Description("故障")]
        GuZhang =2,
        /// <summary>
        /// 报警
        /// </summary>
        [Description("报警")]
        Warning =3,
        /// <summary>
        /// 旁路
        /// </summary>
        [Description("旁路")]
        PangLu =4
    }
}