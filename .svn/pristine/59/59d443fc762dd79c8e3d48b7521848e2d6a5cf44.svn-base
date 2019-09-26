using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Spcms.Dto;

namespace General.Api.Application.Spcms
{
    /// <summary>
    /// 入侵报警服务
    /// </summary>
    public interface ISpcmsService
    {
        /// <summary>
        /// 获取子系统状态
        /// </summary>
        /// <param name="subSysIndexCodes">子系统编码列表</param>
        /// <returns></returns>
        Task<ListBaseResponse<SubSysStatusResponse>> GetSubsystemStatus(List<string> subSysIndexCodes);
        /// <summary>
        /// 获取防区状态
        /// </summary>
        /// <param name="defenceIndexCodes">防区编码列表</param>
        /// <returns></returns>
        Task<ListBaseResponse<DefenceStatusResponse>> GetDefenceStatus(List<string> defenceIndexCodes);
        /// <summary>
        /// 报警事件日志查询
        /// </summary>
        /// <param name="pageNo">当前页码</param>
        /// <param name="pageSize">每页数据记录数</param>
        /// <param name="srcType">事件源类型</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="srcName">事件源名称</param>
        /// <returns></returns>
        Task<ListBaseResponse<EventLogResponse>> GetEventLog(int pageNo, int pageSize, Hikvision.Resource.SrcType srcType,
            DateTime? startTime, DateTime? endTime, Hikvision.Resource.EventWarningType eventType, string srcName);
    }
}