using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Hikvision.Resource;
using General.Api.Application.Spcms;
using General.Api.Application.Spcms.Dto;
using General.Api.Framework.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    /// <summary>
    /// 入侵报警
    /// </summary>
    [Route("api/[controller]"), GeneralAuthorize]
    [ApiController]
    public class SpcmsController : ControllerBase
    {
        private readonly ISpcmsService _spcmsService;
        /// <summary>
        /// construct
        /// </summary>
        public SpcmsController(ISpcmsService spcmsService)
        {
            _spcmsService = spcmsService;
        }

        /// <summary>
        /// 获取子系统状态
        /// </summary>
        /// <param name="subSysIndexCodes">子系统编码列表</param>
        /// <returns></returns>
        [HttpPost, Route("subsystem/status")]
        public async Task<ListBaseResponse<SubSysStatusResponse>> GetSubsystemStatus(List<string> subSysIndexCodes)
        {
            return await _spcmsService.GetSubsystemStatus(subSysIndexCodes);
        }
        /// <summary>
        /// 获取防区状态
        /// </summary>
        /// <param name="defenceIndexCodes">防区编码列表</param>
        /// <returns></returns>
        [HttpPost, Route("defence/status")]
        public async Task<ListBaseResponse<DefenceStatusResponse>> GetDefenceStatus(List<string> defenceIndexCodes)
        {
            return await _spcmsService.GetDefenceStatus(defenceIndexCodes);
        }
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
        [HttpGet, Route("log")]
        public async Task<ListBaseResponse<EventLogResponse>> GetEventLog(int pageNo, int pageSize, SrcType srcType,
            DateTime? startTime, DateTime? endTime, EventWarningType eventType, string srcName)
        {
            return await _spcmsService.GetEventLog(pageNo, pageSize, srcType, startTime, endTime, eventType, srcName);
        }
    }
}