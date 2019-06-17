using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Hikvision.Resource;
using General.Api.Application.Spcms.Dto;
using General.Core;
using General.Core.Extension;
using General.Core.HttpClient.Extension;
using Microsoft.Extensions.Configuration;

namespace General.Api.Application.Spcms
{
    /// <summary>
    /// 入侵报警服务
    /// </summary>
    public class SpcmsService : ISpcmsService
    {
        private readonly string _spcmsApi;
        /// <summary>
        /// construct
        /// </summary>
        public SpcmsService(IConfiguration configuration)
        {
            _spcmsApi = configuration[HikVisionContext.HikVisionBaseApiName];
            if (string.IsNullOrEmpty(_spcmsApi))
            {
                throw new MyException("spcmsApi is null");
            }
        }
        /// <summary>
        /// <see cref="ISpcmsService.GetDefenceStatus(List{string})"/>
        /// </summary>
        public async Task<ListBaseResponse<DefenceStatusResponse>> GetDefenceStatus(List<string> defenceIndexCodes)
        {
            var data = await _spcmsApi.AppendFormatToHik("/api/scpms/v1/eventLogs/searches")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    defenceIndexCodes
                })
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<DefenceStatusResponse>>>();
            return data?.Data;
        }
        /// <summary>
        /// <see cref="ISpcmsService.GetEventLog(int,int,SrcType,DateTime?,DateTime?,EventWarningType,string)"/>
        /// </summary>
        public async Task<ListBaseResponse<EventLogResponse>> GetEventLog(int pageNo, int pageSize, SrcType srcType, DateTime? startTime, DateTime? endTime,
            EventWarningType eventType, string srcName)
        {
            var data = await _spcmsApi.AppendFormatToHik("/api/scpms/v1/eventLogs/searches")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    pageNo,
                    pageSize,
                    srcType = srcType.ToString(),
                    startTime = startTime?.TimeToString(),
                    endTime = endTime?.TimeToString(),
                    eventType,
                    srcName
                })
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<EventLogResponse>>>();
            return data?.Data;
        }

        /// <summary>
        /// <see cref="ISpcmsService.GetSubsystemStatus(List{string})"/>
        /// </summary>
        public async Task<ListBaseResponse<SubSysStatusResponse>> GetSubsystemStatus(List<string> subSysIndexCodes)
        {
            var data = await _spcmsApi.AppendFormatToHik("/api/scpms/v1/subSys/status")
                .SetHiKSecreity()
                .PostAsync(new
                {
                    subSysIndexCodes
                })
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<SubSysStatusResponse>>>();
            return data?.Data;
        }
    }
}