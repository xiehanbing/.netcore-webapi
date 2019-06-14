using System.ComponentModel;
using General.Core.Extension;

namespace General.Api.Application.Spcms.Dto
{
    /// <summary>
    /// 子系统状态
    /// </summary>
    public class SubSysStatusResponse
    {
        /// <summary>
        /// 子系统编码
        /// </summary>
        public string  SubSystemIndexCode { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public SubSysStatusType Status { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDesc => typeof(SubSysStatusType).GetEnumDescription(Status.GetHashCode());
    }
    /// <summary>
    /// 子系统状态
    /// </summary>
    public enum SubSysStatusType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnow =-1,
        /// <summary>
        /// 撤防
        /// </summary>
        [Description("撤防")]
        Che =0,
        /// <summary>
        /// 布放
        /// </summary>
        [Description("布防")]
        Bu =1
    }
}