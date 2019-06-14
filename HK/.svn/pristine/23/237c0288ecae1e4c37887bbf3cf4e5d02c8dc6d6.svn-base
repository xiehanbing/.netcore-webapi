using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Application.Hikvision;
using General.Api.Application.Spcms.Dto;
using General.Core;
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

        public Task<ListBaseResponse<DefenceStatusResponse>> GetDefenceStatus(List<string> defenceIndexCodes)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <see cref="ISpcmsService.GetSubsystemStatus(List{string})"/>
        /// </summary>
        public async Task<ListBaseResponse<SubSysStatusResponse>> GetSubsystemStatus(List<string> subSysIndexCodes)
        {
            var data = await _spcmsApi.AppendFormatToHik("/api/scpms/v1/subSys/status")
                .PostAsync(new
                {
                    subSysIndexCodes
                })
                .ReciveJsonResultAsync<HikVisionResponse<ListBaseResponse<SubSysStatusResponse>>>();
            return data?.Data;
        }
    }
}